using System;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.scheduler;
using Unity.UIWidgets.widgets;

namespace ConnectApp.Components {
    public enum AnimatingType {
        repeat,
        stop,
        reset
    }

    public enum LoadingColor {
        white,
        black
    }

    public enum LoadingSize {
        normal,
        small,
        xSmall
    }

    public class CustomActivityIndicator : StatefulWidget {
        public CustomActivityIndicator(
            Key key = null,
            AnimatingType animating = AnimatingType.repeat,
            LoadingColor loadingColor = LoadingColor.black,
            LoadingSize size = LoadingSize.normal
        ) : base(key: key) {
            this.animating = animating;
            this.loadingColor = loadingColor;
            this.size = size;
        }

        public readonly AnimatingType animating;
        public readonly LoadingColor loadingColor;
        public readonly LoadingSize size;

        public override State createState() {
            return new _CustomActivityIndicatorState();
        }
    }

    public class _CustomActivityIndicatorState : State<CustomActivityIndicator>, TickerProvider {
        AnimationController _controller;

        public override void initState() {
            base.initState();

            this._controller = new AnimationController(
                duration: new TimeSpan(0, 0, 2),
                vsync: this
            );
            if (this.widget.animating == AnimatingType.repeat) {
                this._controller.repeat();
            }
        }

        public Ticker createTicker(TickerCallback onTick) {
            return new Ticker(onTick: onTick, () => $"created by {this}");
        }

        public override void didUpdateWidget(StatefulWidget oldWidget) {
            base.didUpdateWidget(oldWidget: oldWidget);
            if (oldWidget is CustomActivityIndicator customActivityIndicator) {
                if (this.widget.animating != customActivityIndicator.animating) {
                    if (this.widget.animating == AnimatingType.repeat) {
                        this._controller.repeat();
                    }
                    else if (this.widget.animating == AnimatingType.stop) {
                        this._controller.stop();
                    }
                    else if (this.widget.animating == AnimatingType.reset) {
                        this._controller.reset();
                    }
                }
            }
        }

        public override Widget build(BuildContext context) {
            int sideLength;
            switch (this.widget.size) {
                case LoadingSize.normal: {
                    sideLength = 24;
                    break;
                }
                case LoadingSize.small: {
                    sideLength = 20;
                    break;
                }
                case LoadingSize.xSmall:
                default:{
                    sideLength = 16;
                    break;
                }
            }

            return new RotationTransition(
                turns: this._controller,
                child: new Center(
                    child: Image.asset(
                        this.widget.loadingColor == LoadingColor.white
                            ? "image/white-loading"
                            : "image/black-loading",
                        width: sideLength,
                        height: sideLength
                    )
                )
            );
        }

        public override void dispose() {
            this._controller.dispose();
            base.dispose();
        }
    }
}