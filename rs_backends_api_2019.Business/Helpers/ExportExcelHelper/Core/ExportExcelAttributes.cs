using Newtonsoft.Json.Linq;
using rs_backends_api_2019.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Core
{
    public class ExportExcelAttributes
    {
        protected IUnitOfWork _unitOfWork { get; set; }

        protected JObject _parameter { get; set; }

        protected JObject _styles { get; set; }

        protected dynamic resource { get; set; }

        protected int _sheetIndex { get; set; } = 0;

        public virtual void setResources(dynamic resource)
        {
            this.resource = resource;
        }

        public virtual dynamic getResources()
        {
            return this.resource;
        }

        public virtual void setUnitOfWork(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public virtual void setParameter(JObject parameter)
        {
            this._parameter = parameter;
        }

        public virtual void setStyles(object styles)
        {
            this._styles = JObject.FromObject(styles);
        }

        public virtual T getParameter<T>() where T : class
        {
            return (T)this._parameter.ToObject<T>();
        }

        public virtual dynamic param(string key)
        {
            dynamic param = this._parameter;

            return param[key];
        }

        public virtual void setSheetIndex(int index)
        {
            this._sheetIndex = index;
        }

        public virtual int getSheetIndex()
        {
            return this._sheetIndex;
        }
    }
}
