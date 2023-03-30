/// Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved 
/// PROPRIETARY/CONFIDENTIAL 
/// This software is the confidential and proprietary 
/// information of SAMSUNG ELECTRONICS ("Confidential Information"). You shall 
/// not disclose such Confidential Information and shall use it only in 
/// accordance with the terms of the license agreement you entered into with 
/// SAMSUNG ELECTRONICS. SAMSUNG make no representations or warranties about the 
/// suitability of the software, either express or implied, including but not 
/// limited to the implied warranties of merchantability, fitness for a 
/// particular purpose, or non-infringement. SAMSUNG shall not be liable for any 
/// damages suffered by licensee as a result of using, modifying or distributing 
/// this software or its derivatives.


using System;
using System.Threading;
using System.Threading.Tasks;
using Tizen.Uix.Tts;

namespace Tizen.NUI.FLUX.Component
{
    public class TtsService
    {
        private static readonly TtsService instance = new TtsService();
        private TtsClient client = null;

        public static TtsService Instance => instance;

        public TtsService()
        {
        }

        public void Terminate()
        {
            try
            {
                if (client != null)
                {
                    if (client.CurrentState != State.Created && client.CurrentState != State.Unavailable)
                    {
                        // DF180111-01314, DF180110-00361, guide from tts team. KONA attached the mail. 
                        if (client.CurrentState == State.Playing || client.CurrentState == State.Paused)
                        {
                            client.Stop();
                            client.Unprepare();
                        }
                        if (client.CurrentState == State.Ready)
                        {
                            client.Unprepare();
                        }
                    }
                    client.Dispose();
                    client = null;
                }
            }
            catch (Exception e)
            {
                FluxLogger.ErrorP("fail to destroy the TtsClient, throw Exception = %s1", s1: e.Message);
            }
        }

        public bool FlagEnable()
        {
            return System.SystemSettings.AccessibilityTtsEnabled;
        }

        public void Play(string contents)
        {
            
            FluxLogger.InfoP("contents = %s1", s1: contents);
            if (!FlagEnable())
            {
                FluxLogger.InfoP("ttsOn is false do nothing");
                return;
            }

            if (client == null)
            {
                try
                {
                    client = new TtsClient
                    {
                        CurrentMode = Mode.ScreenReader
                    };
                    client.Prepare();
                }
                catch (InvalidOperationException e)
                {
                    FluxLogger.ErrorP("fail to create the TtsClient, throw InvalidOperationException = %s1", s1: e.Message);
                    throw new InvalidOperationException("fail to create the TtsClient, message = " + e.Message);
                }
                catch (Exception e)
                {
                    FluxLogger.ErrorP("fail to create the TtsClient, throw Exception = %s1", s1: e.Message);
                    throw new Exception("fail to create the TtsClient, message = " + e.Message);
                }
            }

            if (contents == null || contents == "")
            {
                contents = " ";
            }

            FluxLogger.InfoP("[BEFORE] ThreadId = %d1", d1: Thread.CurrentThread.ManagedThreadId);

            Task.Run(async () =>
            {
                FluxLogger.InfoP("[AFTER] ThreadId = %d1", d1: Thread.CurrentThread.ManagedThreadId);
                await Run(contents);
            });
        }

        private async Task Run(string contents)
        {
            try
            {
                FluxLogger.InfoP("Contents : %s1, currentState: %s2", s1: contents, s2: FluxLogger.EnumToString(client?.CurrentState));
                switch (client?.CurrentState)
                {
                    case State.Created:
                        client?.Prepare();
                        break;
                    case State.Playing:
                    case State.Paused:
                        client?.Stop();
                        break;
                    default:
                        break;
                }

                if (client?.CurrentState != State.Ready)
                {
                    await WaitFlag(10);
                }

                if (client?.CurrentState == State.Ready)
                {
                    client?.AddText(contents, null, 0, 0);
                    client?.Play();
                }
                else
                {
                    FluxLogger.InfoP("tts is not played current state: %s1", s1: FluxLogger.EnumToString(client?.CurrentState));
                }
            }
            catch (InvalidOperationException e)
            {
                FluxLogger.ErrorP("fail to run TtsClient, throw InvalidOperationException = %s1", s1: e.Message);
                throw new InvalidOperationException("fail to run TtsClient, message = " + e.Message);
            }
            catch (Exception e)
            {
                FluxLogger.ErrorP("fail to run TtsClient, throw InvalidOperationException = %s1", s1: e.Message);
                throw new InvalidOperationException("fail to run TtsClient, message = " + e.Message);
            }
        }

        private async Task WaitFlag(int times)
        {
            int count = 0;
            while (true)
            {
                FluxLogger.InfoP("Wait currentState: %s1 / count: %d1", s1: FluxLogger.EnumToString(client?.CurrentState), d1: count);
                await Task.Delay(1000);
                count++;
                if (client?.CurrentState == State.Ready)
                {
                    break;
                }
                if (count == times)
                {
                    FluxLogger.InfoP("count : %d1   expired", d1: times);
                    break;
                }
            }
        }
    }
}
