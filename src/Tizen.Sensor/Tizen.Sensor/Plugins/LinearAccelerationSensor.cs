/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

namespace Tizen.Sensor
{
    /// <summary>
    /// The LinearAccelerationSensor class is used for registering callbacks for the linear acceleration sensor and getting the linear acceleration data.
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public sealed class LinearAccelerationSensor : Sensor
    {
        private const string LinearAccelerationSensorKey = "http://tizen.org/feature/sensor.linear_acceleration";

        private event EventHandler<SensorAccuracyChangedEventArgs> _accuracyChanged;

        /// <summary>
        /// Get the X component value of the linear acceleration.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <value> X </value>
        public float X { get; private set; } = float.MinValue;

        /// <summary>
        /// Get the Y component value of the linear acceleration.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <value> Y </value>
        public float Y { get; private set; } = float.MinValue;

        /// <summary>
        /// Get the Z component value of the linear acceleration.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <value> Z </value>
        public float Z { get; private set; } = float.MinValue;

        /// <summary>
        /// Return true or false based on whether the linear acceleration sensor is supported by the device.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <value><c>true</c> if supported; otherwise <c>false</c>.</value>
        public static bool IsSupported
        {
            get
            {
                Log.Info(Globals.LogTag, "Checking if the LinearAccelerationSensor is supported");
                return CheckIfSupported(SensorType.LinearAccelerationSensor, LinearAccelerationSensorKey);
            }
        }

        /// <summary>
        /// Return the number of linear acceleration sensors available on the system.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <value> The count of linear acceleration sensors. </value>
        public static int Count
        {
            get
            {
                Log.Info(Globals.LogTag, "Getting the count of linear acceleration sensors");
                return GetCount();
            }
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Tizen.Sensor.LinearAccelerationSensor"/> class.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <feature>http://tizen.org/feature/sensor.linear_acceleration</feature>
        /// <exception cref="ArgumentException">Thrown when an invalid argument is used.</exception>
        /// <exception cref="NotSupportedException">Thrown when the sensor is not supported.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the operation is invalid for the current state.</exception>
        /// <param name='index'>
        /// Index refers to a particular linear acceleration sensor in case of multiple sensors.
        /// Default value is 0.
        /// </param>
        public LinearAccelerationSensor(uint index = 0) : base(index)
        {
            Log.Info(Globals.LogTag, "Creating LinearAccelerationSensor object");
        }

        internal override SensorType GetSensorType()
        {
            return SensorType.LinearAccelerationSensor;
        }

        /// <summary>
        /// An event handler for storing the callback functions for the event corresponding to the change in the linear acceleration sensor data.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandler<LinearAccelerationSensorDataUpdatedEventArgs> DataUpdated;

        /// <summary>
        /// An event handler for accuracy changed events.
        /// If an event is added, a new accuracy change callback is registered for this sensor.
        /// If an event is removed, accuracy change callback is unregistered for this sensor.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandler<SensorAccuracyChangedEventArgs> AccuracyChanged
        {
            add
            {
                if (_accuracyChanged == null)
                {
                    AccuracyListenStart();
                }
                _accuracyChanged += value;
            }
            remove
            {
                _accuracyChanged -= value;
                if (_accuracyChanged == null)
                {
                    AccuracyListenStop();
                }
            }
        }

        private static int GetCount()
        {
            IntPtr list;
            int count;
            int error = Interop.SensorManager.GetSensorList(SensorType.LinearAccelerationSensor, out list, out count);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error getting sensor list for linear acceleration sensor");
                count = 0;
            }
            else
                Interop.Libc.Free(list);
            return count;
        }

        /// <summary>
        /// Read linear acceleration sensor data synchronously.
        /// </summary>
        internal override void ReadData()
        {
            Interop.SensorEventStruct sensorData;
            int error = Interop.SensorListener.ReadData(ListenerHandle, out sensorData);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error reading linear acceleration sensor data");
                throw SensorErrorFactory.CheckAndThrowException(error, "Reading linear acceleration sensor data failed");
            }

            Timestamp = sensorData.timestamp;
            X = sensorData.values[0];
            Y = sensorData.values[1];
            Z = sensorData.values[2];
        }

        private static Interop.SensorListener.SensorEventsCallback _callback;

        internal override void EventListenStart()
        {
            _callback = (IntPtr sensorHandle, IntPtr eventPtr, uint events_count, IntPtr data) =>
            {
                updateBatchEvents(eventPtr, events_count);
                Interop.SensorEventStruct sensorData = latestEvent();

                Timestamp = sensorData.timestamp;
                X = sensorData.values[0];
                Y = sensorData.values[1];
                Z = sensorData.values[2];

                DataUpdated?.Invoke(this, new LinearAccelerationSensorDataUpdatedEventArgs(sensorData.values));
            };

            int error = Interop.SensorListener.SetEventsCallback(ListenerHandle, _callback, IntPtr.Zero);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error setting event callback for linear acceleration sensor");
                throw SensorErrorFactory.CheckAndThrowException(error, "Unable to set event callback for linear acceleration sensor");
            }
        }

        internal override void EventListenStop()
        {
            int error = Interop.SensorListener.UnsetEventsCallback(ListenerHandle);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error unsetting event callback for linear acceleration sensor");
                throw SensorErrorFactory.CheckAndThrowException(error, "Unable to unset event callback for linear acceleration");
            }
        }

        private static Interop.SensorListener.SensorAccuracyCallback _accuracyCallback;

        private void AccuracyListenStart()
        {
            _accuracyCallback = (IntPtr sensorHandle, UInt64 timestamp, SensorDataAccuracy accuracy, IntPtr data) =>
            {
                Timestamp = timestamp;
                _accuracyChanged?.Invoke(this, new SensorAccuracyChangedEventArgs(timestamp, accuracy));
            };

            int error = Interop.SensorListener.SetAccuracyCallback(ListenerHandle, _accuracyCallback, IntPtr.Zero);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error setting accuracy event callback for linear acceleration sensor");
                throw SensorErrorFactory.CheckAndThrowException(error, "Unable to set accuracy event callback for linear acceleration sensor");
            }
        }

        private void AccuracyListenStop()
        {
            int error = Interop.SensorListener.UnsetAccuracyCallback(ListenerHandle);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error unsetting accuracy event callback for linear acceleration sensor");
                throw SensorErrorFactory.CheckAndThrowException(error, "Unable to unset accuracy event callback for linear acceleration sensor");
            }
        }
    }
}
