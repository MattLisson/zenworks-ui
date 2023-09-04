using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using NodaTime;
using SkiaSharp.Views.Maui;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using System.Runtime.Versioning;

namespace Zenworks.UI {

    [SupportedOSPlatform("ios")]
    public class CountdownClockView : SKCanvasView {

        public static readonly BindableProperty TimeRemainingProperty = BindableProperty.Create(
            nameof(TimeRemaining),
            typeof(Duration),
            typeof(CountdownClockView),
            Duration.Zero,
            defaultBindingMode: BindingMode.OneWay,
            null,
            OnPropertyChanged);

        public static readonly BindableProperty TotalTimeProperty = BindableProperty.Create(
            nameof(TotalTime),
            typeof(Duration),
            typeof(CountdownClockView),
            Duration.FromSeconds(1),
            defaultBindingMode: BindingMode.OneWay,
            null,
            OnPropertyChanged);

        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(
            nameof(Thickness),
            typeof(float),
            typeof(CountdownClockView),
            22.0f,
            defaultBindingMode: BindingMode.OneWay,
            null,
            OnPropertyChanged);

        public static readonly BindableProperty BackgroundAlphaProperty = BindableProperty.Create(
            nameof(BackgroundAlpha),
            typeof(float),
            typeof(CountdownClockView),
            0.25f,
            defaultBindingMode: BindingMode.OneWay,
            null,
            OnPropertyChanged);

        public static readonly BindableProperty ForegroundColorProperty = BindableProperty.Create(
            nameof(ForegroundColor),
            typeof(Color),
            typeof(CountdownClockView),
            new Color(0xA3, 0xA1, 0xFB),
            defaultBindingMode: BindingMode.OneWay,
            null,
            OnPropertyChanged);

        public Duration TimeRemaining {
            get => (Duration)GetValue(TimeRemainingProperty);
            set => SetValue(TimeRemainingProperty, Duration.Max(value, Duration.Zero));
        }

        public Duration TotalTime {
            get => (Duration)GetValue(TotalTimeProperty);
            set => SetValue(TotalTimeProperty, value);
        }

        public float Thickness {
            get => (float)GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }
        public float BackgroundAlpha {
            get => (float)GetValue(BackgroundAlphaProperty);
            set => SetValue(BackgroundAlphaProperty, value);
        }

        public Color ForegroundColor {
            get => (Color)GetValue(ForegroundColorProperty);
            set => SetValue(ForegroundColorProperty, value);
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e) {
            base.OnPaintSurface(e);
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            int width = info.Width;
            int height = info.Height;
            int centerY = height / 2;
            int centerX = width / 2;

            float radius = (Math.Min(width, height) - Thickness) / 2;
            float arcAngle = (float)(360.0 * (TimeRemaining / TotalTime));


            SKPaint backgroundPaint = new SKPaint {
                Style = SKPaintStyle.Stroke,
                Color = ForegroundColor.ToSKColor().WithAlpha((byte)(BackgroundAlpha * 255)),
                StrokeWidth = Thickness,
            };
            SKPaint foregroundPaint = new SKPaint {
                Style = SKPaintStyle.Stroke,
                Color = ForegroundColor.ToSKColor(),
                StrokeWidth = Thickness,
                IsAntialias = true,
            };

            canvas.DrawCircle(centerX, centerY, radius, backgroundPaint);

            SKPath partialCircle = new SKPath();
            SKRect circleArea = new SKRect(centerX - radius, centerY - radius, centerX + radius, centerY + radius);
            partialCircle.AddArc(circleArea, -90.0f, arcAngle);
            canvas.DrawPath(partialCircle, foregroundPaint);

            SKPaint textPaint = new SKPaint {
                Color = SKColors.Black.WithAlpha(170),
                TextSize = 38.0f,
                IsStroke = false,
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
            };

            canvas.DrawText($"{TimeRemaining.FormatS()}s", centerX, centerY + 19.0f, textPaint);
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            if (bindable is CountdownClockView clock) {
                clock.InvalidateSurface();
            }
        }
    }
}
