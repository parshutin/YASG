using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Contexts;
using strange.extensions.context.impl;

namespace Assets.Scripts
{
    public class MenuRoot : ContextView
    {
        void Awake()
        {
            context = new MenuContext(this);
        }
    }
}
