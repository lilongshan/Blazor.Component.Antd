﻿using LuanNiao.Blazor.Component.Antd.Icons;
using LuanNiao.Blazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuanNiao.Blazor.Component.Antd.Radio
{
    public partial class LNRadioItem
    {
        [Parameter]
        public bool Disabled { get; set; }
        [Parameter]
        public bool Checked { get; set; }
        [Parameter ]
        public int Value { get; set; }
        [CascadingParameter]
        public LNRadioGroup Group { get; set; }

        private const string _checkClass = "ant-radio-checked";
        private const string _disabledClass = "ant-radio-disabled";

        public LNRadioItem()
        {
            _classHelper.SetStaticClass("ant-radio");
        }
        protected override void OnParametersSet()
        {
            _classHelper.AddCustomClass(_checkClass, () => Checked)
              .AddOrRemove(_disabledClass, condition: () => Disabled);
            this.Flush();
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Group.ItemSelected += GotOtherItemSelectedEvent;
        }

        private void HandleClickEvent()
        {
            if (this.Disabled)
            {
                return;
            }
            Checked = !Checked;
            this._classHelper.AddCustomClass(_checkClass);
            Group.Triggered(this);
            this.Flush();
        }
 
        private void GotOtherItemSelectedEvent(LNRadioItem targetItem)
        {
            if (targetItem.Equals(this))
            {
                Group._checkValue = this.Value;
                return;
            }
            this._classHelper.RemoveCustomClass(_checkClass);
            this.Flush();
        }    
    }
}