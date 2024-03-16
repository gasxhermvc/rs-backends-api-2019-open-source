using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rs_backends_api_2019.DAL.Models.Defaults.PaginationModel
{
    public class Pagination<T> where T : class
    {
        public int CurrentPage { get; set; } = 1;

        public dynamic Data { get; set; } = null;

        public dynamic Items { get; set; } = new List<T>();

        public string FirstPageUrl { get; set; }

        public int From { get; set; } = 0;

        public int LastPage { get; set; } = 1;

        public string LastPageUrl { get; set; }

        public string NextPageUrl { get; set; }

        public string Path { get; set; }

        public int PageSize { get; set; } = 20;

        public string PrevPageUrl { get; set; }

        public int To { get; set; } = 0;

        public int Total { get; set; } = 0;

        public bool HasPages { get; set; } = false;

        /// <summary>
        /// สำหรับเช็ทค่า Items ใหม่ให้กับของเดิม
        /// </summary>
        /// <param name="items"></param>
        private void CompactItems(object items)
        {
            if (!(items is IEnumerable))
            {
                throw new ArgumentException("{0} : Sorry, required type of Collection or IEnumerable only", nameof(items));
            }

            if (!items.GetType().IsArray && !items.GetType().IsGenericType)
            {
                throw new ArgumentException("{0} : Sorry, required type of Collection or IEnumerable only", nameof(items));
            }

            this.Data = null;
            this.Items = (object)items;
        }

        /// <summary>
        /// สำหรับเช็ทค่า data ใหม่ให้กับของเดิม
        /// </summary>
        /// <param name="data"></param>
        private void CompactModel(object model)
        {
            Dictionary<string, dynamic> compact = new Dictionary<string, dynamic>();
            var objectNames = model.GetType().GetProperties().ToList();

            foreach (var obj in objectNames)
            {
                if (obj.GetValue(model) != this.Items)
                {
                    compact.Add(obj.Name, obj.GetValue(model));
                }
            }

            this.Data = (object)compact;
        }

        /// <summary>
        /// สำหรับเช็ทค่า Data และ Items ใหม่ให้กับของเดิม
        /// </summary>
        /// <param name="model"></param>
        /// <param name="items"></param>
        public void Compact(object model = null, object items = null)
        {
            if (model != null && items != null)
            {
                CompactDataAndItems(model, items);
            }
            else if (model != null && items == null)
            {
                CompactModel(model);
            }
            else if (items != null && model == null)
            {
                CompactItems(items);
            }
        }

        public void CompactDataAndItems(object model, object items)
        {
            if (!(items is IEnumerable))
            {
                throw new ArgumentException("{0} : Sorry, required type of Collection or IEnumerable only", nameof(items));
            }

            if (!items.GetType().IsArray && !items.GetType().IsGenericType)
            {
                throw new ArgumentException("{0} : Sorry, required type of Collection or IEnumerable only", nameof(items));
            }

            Dictionary<string, dynamic> compact = new Dictionary<string, dynamic>();
            var objectNames = model.GetType().GetProperties().ToList();

            foreach (var obj in objectNames)
            {
                if (obj.GetValue(model) != items)
                {
                    compact.Add(obj.Name, obj.GetValue(model));
                }
            }

            this.Data = (object)compact;
            this.Items = items;
        }
    }
}
