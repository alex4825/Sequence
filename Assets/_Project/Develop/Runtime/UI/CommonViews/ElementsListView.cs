using Assets._Project.Develop.Runtime.UI.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.CommonViews
{
    public class ElementsListView<TElement> : MonoBehaviour, IView where TElement : MonoBehaviour, IView
    {
        [SerializeField] private Transform _parent;

        private List<TElement> _elements = new();

        public IReadOnlyList<TElement> Elements => _elements;

        public void Add(params TElement[] elements)
        {
            foreach (var element in elements)
                element.transform.SetParent(_parent, false);

            _elements.AddRange(elements);
        }

        public void Remove(TElement element)
        {
            element.transform.SetParent(null, false);
            _elements.Remove(element);
        }
    }
}
