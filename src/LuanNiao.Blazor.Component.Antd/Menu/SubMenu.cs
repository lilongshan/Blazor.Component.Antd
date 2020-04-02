﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuanNiao.Blazor.Core;
using Microsoft.AspNetCore.Components;

namespace LuanNiao.Blazor.Component.Antd.Menu
{
    public partial class SubMenu : LNBCBase
    {
        private const string _disableClassName = "ant-menu-item-disabled";
        private const string _openClassName = "ant-menu-submenu-open";
        private const string _activeClassName = "t-menu-submenu-active";
        private const string _hideDivClassName = "ant-menu-submenu-hidden";
        private const string _hidULClassName = "ant-menu-hidden";
        public SubMenu()
        {
            _classHelper.SetStaticClass("ant-menu-submenu");
            HideSubMenuDivClassName = _hideSubMenuDivClassNameHelper.AddCustomClass(_hideDivClassName).Build();
            HideSubMenuULClassName = _hideSubMenuULClassNameHelper.AddCustomClass(_hidULClassName).Build();
        }

        [Inject]
        public ElementInfo ElementHelper { get; set; }

        [Parameter]
        public string PopupClassName { get; set; }

        [Parameter]
        public List<MenuBase> Children { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public RenderFragment Title { get; set; }
        [Parameter]
        public Action<SubMenu> OnTitleClick { get; set; }

        private readonly ClassNameHelper _hideSubMenuDivClassNameHelper = new ClassNameHelper()
            .SetStaticClass("ant-menu-submenu ant-menu-submenu-popup ant-menu-light ant-menu-submenu-placement-bottomLeft")
            .AddCustomClass(_hideDivClassName);
        private readonly ClassNameHelper _hideSubMenuULClassNameHelper = new ClassNameHelper()
            .SetStaticClass("ant-menu ant-menu-sub  ant-menu-vertical ")
            .AddCustomClass(_hidULClassName);
        private string HideSubMenuDivClassName { get; set; }
        private string HideSubMenuULClassName { get; set; }

        private readonly OriginalStyleHelper _hideSubMenuDivStyle = new OriginalStyleHelper();
        private string HideSubMenuDivStyle { get; set; }


        private readonly OriginalStyleHelper _hideSubMenuULStyle = new OriginalStyleHelper();
        private string HideSubMenuULStyle { get; set; }



        protected override void OnInitialized()
        {
            HandleParent();
        }
        private void HandleParent()
        {
            if (this.Parent is HorizontalMenu)
            {
                _classHelper.AddCustomClass("ant-menu-submenu-horizontal");
            }
        }
        private async void OnMouseOver()
        {
            if (string.IsNullOrWhiteSpace(HideSubMenuDivStyle))
            {
                var elementInfo = await ElementHelper.GetElementRectsByID($"{IdentityKey}_mainli");
                HideSubMenuDivStyle = _hideSubMenuDivStyle.AddCustomStyle("left", $"{elementInfo.Left}px").AddCustomStyle("top", $"{elementInfo.Bottom + 2}px").Build();
                HideSubMenuULStyle = _hideSubMenuULStyle.AddCustomStyle("min-width", $"{elementInfo.Width}px").Build();
            }
            _classHelper
                .AddCustomClass(_openClassName)
                .AddCustomClass(_activeClassName);
            HideSubMenuDivClassName = _hideSubMenuDivClassNameHelper.RemoveCustomClass(_hideDivClassName).Build();
            HideSubMenuULClassName = _hideSubMenuULClassNameHelper.RemoveCustomClass(_hidULClassName).Build();
            this.Flush();
        }

        private void OnMouseOut()
        {
            _classHelper
                .RemoveCustomClass(_openClassName)
                .RemoveCustomClass(_activeClassName);
            HideSubMenuDivClassName = _hideSubMenuDivClassNameHelper.AddCustomClass(_hideDivClassName).Build();
            HideSubMenuULClassName = _hideSubMenuULClassNameHelper.AddCustomClass(_hidULClassName).Build();
        }

    }
}
