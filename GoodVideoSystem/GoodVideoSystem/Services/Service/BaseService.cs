using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Services.Service
{
    public abstract class BaseService:IDisposable

    {
        public IList<IDisposable> DisposableObject { get; private set; }

        public BaseService()
        {
            this.DisposableObject = new List<IDisposable>();
        }

        public void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (disposable != null)
            {
                this.DisposableObject.Add(disposable);
            }
        }

        public void Dispose()
        { 
            foreach(IDisposable obj in this.DisposableObject)
            {
                if(obj != null)
                {
                    obj.Dispose();
                }
            }
        }
    }
}