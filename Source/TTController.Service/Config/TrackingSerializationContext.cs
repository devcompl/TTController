﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTController.Service.Utils;

namespace TTController.Service.Config
{
    public class TrackingSerializationContext
    {
        private readonly Dictionary<Type, List<object>> _objects;

        public TrackingSerializationContext()
        {
            _objects = new Dictionary<Type, List<object>>();
        }

        public void Track(Type type)
        {
            if(!_objects.ContainsKey(type))
                _objects.Add(type, new List<object>());
        }

        public void Handle(object o)
        {
            var key = _objects.Keys.FirstOrDefault(k => k.IsAssignableOrSubclass(o.GetType()));
            if (key == null)
                return;

            _objects[key].Add(o);
        }

        public IEnumerable<T> Get<T>()
            => _objects.Keys
            .Where(k => k.IsAssignableOrSubclass(typeof(T)))
            .SelectMany(t => _objects[t]).OfType<T>();
    }
}
