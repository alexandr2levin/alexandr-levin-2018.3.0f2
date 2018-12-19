using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Base
{
    public class Screen<TView, TPresenter> where TView : IView where TPresenter : Presenter<TView>
    {
        protected TPresenter Presenter;
    }
}