﻿using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using IntraVision.Web.Toolkit;

namespace IntraVision.Web.Mvc.Controls
{
    public class UserRelationFilterCondition<TEntity> : IFilterCondition<TEntity>
        where TEntity : class, new()
    {
        string _Caption;
        string _Value;
        string _ChildProperty;
        string _ParentField;
        string _AutoCompleteUrl;
        string _SelectUrl;
        int _UserId = 0;

        string _ValueField;
        string _DisplayField;
        IQueryable _Dictionary;
        
        public string Column
        {
            get { return _ParentField; }
            set { _ParentField = value; }
        }

        public bool Active
        {
            get { return _Type > 0 && !string.IsNullOrEmpty(_Value); }
        }

        int _Type = 0;

        string[] _Types = { HTMLHelper.Resource("Filter", "Equal", "равно"), HTMLHelper.Resource("Filter", "NotEqual", "не равно"), HTMLHelper.Resource("Filter", "ContainsAny", "содержит любое из"), HTMLHelper.Resource("Filter", "NotContainsAny", "не содержит любое из"), HTMLHelper.Resource("Filter", "ContainsAll", "содержит все из"), HTMLHelper.Resource("Filter", "ContainsNone", "не содержит все из"), HTMLHelper.Resource("Filter", "Defined", "задано"), HTMLHelper.Resource("Filter", "Undefined", "не задано") };

        public UserRelationFilterCondition(string childProperty, string parentField, string caption, IQueryable dictionary, int userid, string acurl, string selurl)
            : this(childProperty, parentField, caption, dictionary, userid, acurl,selurl,"Id", "Name")
        { }

        public UserRelationFilterCondition(string childProperty, string parentField, string caption, IQueryable dictionary, int userid, string acurl, string selurl, string valuefield, string displayfield)
        {
            _Caption = caption;
            _ChildProperty = childProperty;
            _ParentField = parentField;
            _Dictionary = dictionary;
            _UserId = userid;
            _AutoCompleteUrl = acurl;
            _SelectUrl = selurl;
            _ValueField = valuefield;
            _DisplayField = displayfield;
        }

        #region IFilterCondition<TEntity> Members

        public IQueryable<TEntity> Filter(IQueryable<TEntity> list)
        {
            if (_Type < 1) return list;
            if (_Type == 7) return list.Where(string.Format(CultureInfo.InvariantCulture, "{0}.Count() > 0", _ChildProperty));
            if (_Type == 8) return list.Where(string.Format(CultureInfo.InvariantCulture, "{0}.Count() == 0", _ChildProperty));

            string clause = "";
            int[] ids = RequestHelper.GetIdsFromString(_Value);
            if (ids != null && ids.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                string op = "==";
                string dv = "||";
                switch (_Type)
                {
                    case 1:
                        clause = string.Format(CultureInfo.InvariantCulture, "=={0} && {1}.Count() == {0}", ids.Length, _ChildProperty);
                        break;
                    case 2:
                        clause = string.Format(CultureInfo.InvariantCulture, "<{0} || {1}.Count() > {0}", ids.Length, _ChildProperty);
                        break;
                    case 3:
                        clause = ">0";
                        break;
                    case 4:
                        clause = "==0";
                        break;
                    case 5:
                        clause = string.Format(CultureInfo.InvariantCulture, "=={0}", ids.Length);
                        break;
                    case 6:
                        clause = string.Format(CultureInfo.InvariantCulture, "<{0}", ids.Length);
                        break;
                }
                foreach (int id in ids)
                {
                    if (sb.Length > 0) sb.Append(dv);
                    sb.Append(_ParentField).Append(op).Append(id > 0 ? id : _UserId);
                }
                string where = string.Format(CultureInfo.InvariantCulture, "{0}.Where({1}).Count() {2}", _ChildProperty, sb.ToString(), clause);
                return list.Where(where);
            }
            return list;
        }

        #endregion

        #region IFilterCondition Members

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div class=""filterfield""><table width=""100%""><tr><td class=""tdlabel""><label>").Append(_Caption).AppendFormat("&nbsp;<a class=\"small\" href=\"{0}\" onclick=\"openDialog('{0}'); return false;\">[?]</a></label></td>", HTMLHelper.Resource("Filter", "RelatedFilterHelp", "help/related_filter.htm"));
            sb.Append(string.Format(CultureInfo.InvariantCulture, @"<td class=""tdcondition""><select class=""flttype fttrelation"" id=""ftt{0}"" name=""ftt{0}""><option value=""0"" />", _ChildProperty));
            for (int i = 0; i < _Types.Length; i++)
                sb.Append(string.Format(CultureInfo.InvariantCulture, @"<option value=""{0}"" {2}>{1}</option>", i + 1, _Types[i], ((_Type == (i + 1)) ? " selected" : "")));
            sb.Append("</select></td>");
            int[] ids = RequestHelper.GetIdsFromString(_Value);
            //В зависимости от количества пользователей возможны два варианта: 
            //1.Вывести сразу всех, если их мало
            //2.Вывести только выбранных, если их много
            DataTable dt = DataBinder.EvalDataSource(_Dictionary, _DisplayField, _ValueField);
            bool few = _Dictionary.Count() <= 10;
            string hidden = (_Type == 8 || _Type == 7) ? "class=\"hidden\"" : "";
            if (!few)
                sb.AppendFormat(CultureInfo.InvariantCulture, @"<td class=""tdvalue""><div {4}><select id=""flt{0}"" name=""flt{0}"" multiple=""multiple"" title=""{1}"" class=""fltselect multipleselect"" acurl=""{2}"" selurl=""{3}"">", _ChildProperty, _Caption, _AutoCompleteUrl, _SelectUrl, hidden);
            else
                sb.AppendFormat(CultureInfo.InvariantCulture, @"<td class=""tdvalue""><div {2}><select id=""flt{0}"" name=""flt{0}"" multiple=""multiple"" title=""{1}"" class=""fltselect multipleselect"">", _ChildProperty, _Caption, hidden);
            sb.AppendFormat(CultureInfo.InvariantCulture, @"<option value=""0"" {0}>{1}</option>", ids.Contains(0) ? " selected=\"selected\"" : "", HTMLHelper.Resource("Filter", "CurrentUser", "Текущий пользователь"));
            foreach (DataRow dr in dt.Rows)
            {
                int id = Convert.ToInt32(dr[_ValueField], CultureInfo.InvariantCulture);
                bool selected = ids.Contains(id);
                if (!few && !selected) continue;
                sb.Append(String.Format(CultureInfo.InvariantCulture, @"<option value=""{0}"" {2}>{1}</option>", Convert.ToString(dr[_ValueField], CultureInfo.InvariantCulture), Convert.ToString(dr[_DisplayField], CultureInfo.InvariantCulture), selected ? "selected=\"true\"" : ""));
            }
            sb.Append("</select></div></td></tr></table></div>");
            return sb.ToString();
        }

        public void Init(NameValueCollection req)
        {
            _Value = req["flt" + _ChildProperty];
            if (!string.IsNullOrEmpty(req["ftt" + _ChildProperty]))
                Int32.TryParse(req["ftt" + _ChildProperty], out _Type);
        }

        #endregion

        public override string ToString()
        {
            if (_Type <= 0) return string.Empty;
            return string.Format("ftt{0}={1}&flt{0}={2}", _ChildProperty, _Type, _Value);
        }
    }
}
