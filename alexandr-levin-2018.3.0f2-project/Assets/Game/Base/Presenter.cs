using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Base
{
    public class Presenter<TView> where TView : IView
    {
        protected TView View;

        public void Attach(TView view)
        {
            // TODO implement here
        }

        public void Detach()
        {
            // TODO implement here
        }
    }
}