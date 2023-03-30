
using System;
using Tizen.System;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// AudioFeedback Class. This class has the methods the Audio Feedback.
    /// </summary>
    /// <code>
    /// AudioFeedback.Instance.Play(AudioFeedback.Pattern.MoveNavigation);
    /// AudioFeedback.Instance.Play(AudioFeedback.Pattern.Select);
    /// </code>
    internal class AudioFeedback : IDisposable
    {
        /// <summary>
        /// Enumerations of the system pre-defined patterns for feedback interface
        /// </summary>
        public enum Pattern
        {
            /// <summary>
            /// feedback pattern when navigation moves
            /// </summary>
            // TODO: Disable Tizen.TV.System.Manager
            //MoveNavigation = FeedbackPattern.MoveNavigation,
            MoveNavigation,

            /// <summary>
            /// feedback pattern when select positive action
            /// </summary>
            // TODO: Disable Tizen.TV.System.Manager
            //Select = FeedbackPattern.Select
            Select
        }

        /// <summary>
        /// Instance of AudioFeedback.
        /// </summary>
        public static AudioFeedback Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Plays various types of reactions that are pre-defined
        /// This functon can be used to react to pre-defined actions
        /// It play various types of system pre-defined media or vibration patterns
        /// </summary>
        /// <param name="pattern">The pre-defined pattern</param>
        /// <returns>0 on success, otherwise a negative error value</returns>
        public int Play(Pattern pattern)
        {
            try
            {
                var patternName = pattern == Pattern.Select ? "Tap" : "KeyBack";
                feedback.Play(FeedbackType.Sound, patternName);
                return 0;
            }
            catch (Exception e)
            {
                FluxLogger.ErrorP("Failed to play audio feedback:[%s1]", s1: e.Message);
                return -1;
            }
        }

        /// <summary>
        /// Stop various types of reactions
        /// This functon can be used to stop react to pre-defined actions
        /// It stops system pre-defined vibration patterns
        /// </summary>
        /// <returns>0 on success, otherwise a negative error value</returns>
        public int Stop()
        {
            try
            {
                feedback.Stop();
                return 0;
            }
            catch (Exception e)
            {
                FluxLogger.ErrorP("Failed to stop audio feedback:[%s1]", s1: e.Message);
                return -1;
            }
        }
        /// <summary>
        /// Dispose AudioFeedback.
        /// </summary>
        public void Dispose()
        {
        }

        private static readonly AudioFeedback instance = new AudioFeedback();

        private AudioFeedback()
        {
            feedback = new Feedback();
        }

        private readonly Feedback feedback;
    }
}

