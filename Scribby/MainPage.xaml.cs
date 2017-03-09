﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Capture;
using Windows.ApplicationModel;
using System.Threading.Tasks;
using Windows.System.Display;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Devices.Sensors;
using System.Diagnostics;
using Windows.Devices.Geolocation;

/// <NOTE TO SELF>
/// render transform takes absolute corrdinates and hence we should an incrementer function to updates its value
/// </summary>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Scribby
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Magnetometer mg;  
        MediaCapture _mediaCapture;
        bool _isPreviewing;
        double yawangle = 330;
        double pitcAngle = 290;
        DisplayRequest _displayRequest;
        Compass c;
        Image im;
        int i;
        double yaw;
        double x, y;
        OrientationSensor or;
        DispatcherTimer tim;
        double wid, h, stepW,stepH,temppitch,tempyaw;
        double yaw5, pitch5;
        
        public MainPage()
        {
            i = 0;
            yaw5 = 0;pitch5 = 0;
            or = OrientationSensor.GetDefault();
            mg = Magnetometer.GetDefault();
            c = Compass.GetDefault();
            //mg.ReadingChanged += Mg_ReadingChanged;
            c.ReportInterval = 4;
            c.ReadingChanged += C_ReadingChanged;
            or.ReportInterval = 4;
            or.ReadingChanged += Or_ReadingChanged;
            this.InitializeComponent();
            tim = new DispatcherTimer();
            tim.Interval = new TimeSpan(1000);
            tim.Tick += Tim_Tick;
            //tim.Start();
            im = new Image();
            im.Width = 60;
            im.Height = 60;
            Loaded += MainPage_Loaded;
            PreviewControl.Loaded += PreviewControl_Loaded;
            Application.Current.Suspending += Application_Suspending;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

        }

        private void C_ReadingChanged(Compass sender, CompassReadingChangedEventArgs args)
        {
            CompassReading reading = args.Reading;
             yaw = reading.HeadingTrueNorth.Value;
            //Debug.WriteLine(lol.ToString());
        }

        private void Mg_ReadingChanged(Magnetometer sender, MagnetometerReadingChangedEventArgs args)
        {
        }

        private void Tim_Tick(object sender, object e)
        {
            TranslateTransform t = new TranslateTransform();
            //temppitch = pitch;temproll = roll;
            
            t.X = 100+i/2;
            i++;
            t.Y = i/2;
            //x = t.X;y = t.Y;
            //Debug.WriteLine("lol");
            im.RenderTransform = t;
        }

        private void PreviewControl_Loaded(object sender, RoutedEventArgs e)
        {
            wid = PreviewControl.ActualWidth;
            h = PreviewControl.ActualHeight;
            OrientationSensorReading reading = or.GetCurrentReading();
            stepH = h / 90;
            stepW = wid / 90;
            SensorQuaternion q = reading.Quaternion;   
            // get a reference to the object to avoid re-creating it for each access
            double ysqr = q.Y * q.Y;
            // roll (x-axis rotation)
            double t0 = +2.0 * (q.W * q.X + q.Y * q.Z);
            double t1 = +1.0 - 2.0 * (q.X * q.X + ysqr);
            double roll = Math.Atan2(t0, t1);
            roll = roll * 180 / Math.PI;
            // pitch (y-axis rotati)
            double t2 = +2.0 * (q.W * q.Y - q.Z * q.X);
            t2 = t2 > 1.0 ? 1.0 : t2;
            t2 = t2 < -1.0 ? -1.0 : t2;
            double pitch = Math.Asin(t2);
            pitch = pitch * 180 / Math.PI;

            

            TranslateTransform t = new TranslateTransform();
            temppitch = pitch;
            tempyaw = yaw;
            im.Source = new BitmapImage(new Uri("https://img.clipartfest.com/3c8ee7ee52b0c2385df4d27ad0f39270_10-facebook-like-thumbs-up-png-facebook-clipart-transparent-background_570-597.png"));
            lol.Children.Add(im);
            t.X = (Math.Abs(yawangle-yaw))*stepW;
            t.Y = (Math.Abs(pitcAngle - pitch)) * stepH;
            x = t.X;y = t.Y;
            //Debug.WriteLine("lol");
            im.RenderTransform = t;
        }
        
        private async void Or_ReadingChanged(OrientationSensor sender, OrientationSensorReadingChangedEventArgs args)
        {
            //check angle here
            OrientationSensorReading reading = args.Reading;

            // Quaternion values
            SensorQuaternion q = reading.Quaternion;   // get a reference to the object to avoid re-creating it for each access
            double ysqr = q.Y * q.Y;
            // roll (x-axis rotation)
            double t0 = +2.0 * (q.W * q.X + q.Y * q.Z);
            double t1 = +1.0 - 2.0 * (q.X * q.X + ysqr);
            double roll = Math.Atan2(t0, t1);
            roll = roll * 180 / Math.PI;

            // pitch (y-axis rotati)
            double t2 = +2.0 * (q.W * q.Y - q.Z * q.X);
            t2 = t2 > 1.0 ? 1.0 : t2;
            t2 = t2 < -1.0 ? -1.0 : t2;
            double pitch = Math.Asin(t2);
            pitch = pitch * 180 / Math.PI;
            // yaw (z-axis rotation)
           

            yaw5 += yaw;
            pitch5 += pitch;
            i++;
            if (i == 14)
            {
                yaw = yaw5 / 15;
                pitch = pitch5 /15;
                yaw5=pitch5=i = 0;
                if (yaw < 0)
                    yaw += 360;
                        if (pitch < 0)
                    pitch += 360;
               // Debug.WriteLine(yaw.ToString() + "," + pitch.ToString());


                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {

                    TranslateTransform t = new TranslateTransform();
                    x = (tempyaw - yaw) * stepW;
                    y = (temppitch - pitch) * stepH;
                    im.RenderTransform = t;
                    temppitch = pitch;
                    tempyaw = yaw;
                    t.X = (Math.Abs(yawangle - yaw)) * stepW;
                    t.Y = (Math.Abs(pitcAngle - pitch)) * stepH;
                });
            }
           Debug.WriteLine(x.ToString()+","+y.ToString());
        }

        private async void Application_Suspending(object sender, SuspendingEventArgs e)
        {
            // Handle global application events only if this page is active
            if (Frame.CurrentSourcePageType == typeof(MainPage))
            {
                var deferral = e.SuspendingOperation.GetDeferral();
                await CleanupCameraAsync();
                deferral.Complete();
            }
        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await StartPreviewAsync();
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            await CleanupCameraAsync();
        }
        private async Task StartPreviewAsync()
        {
            try
            {

                _mediaCapture = new MediaCapture();
                await _mediaCapture.InitializeAsync();   
                PreviewControl.Source = _mediaCapture;
                
                await _mediaCapture.StartPreviewAsync();
                _isPreviewing = true;
                
                _displayRequest.RequestActive();
                DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            }
            catch (UnauthorizedAccessException)
            {
                // This will be thrown if the user denied access to the camera in privacy settings
                System.Diagnostics.Debug.WriteLine("The app was denied access to the camera");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("MediaCapture initialization failed. {0}", ex.Message);
            }
        }
        private async Task CleanupCameraAsync()
        {
            if (_mediaCapture != null)
            {
                if (_isPreviewing)
                {
                    await _mediaCapture.StopPreviewAsync();
                }

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    PreviewControl.Source = null;
                    if (_displayRequest != null)
                    {
                        _displayRequest.RequestRelease();
                    }

                    _mediaCapture.Dispose();
                    _mediaCapture = null;
                });
            }

        }
         
    }
}
