using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue_", menuName = "Create Dialogue Asset")]
    public class Dialogue : ScriptableObject
    {
        [TextArea(4, 16)]
        public string text;

        public char pageSplitter;

        private string[] pages;

        /// <summary>
        /// Creates the pages from the text for this dialogue.
        /// </summary>
        public void Create()
        {
            pages = text.Split(pageSplitter);
        }

        /// <summary>
        /// Returns the text of a specific page of this dialogue. This is 0 indexed!
        /// </summary>
        /// <param name="_page"></param>
        /// <returns></returns>
        public string GetPage(int _page)
        {
            if(_page >= pages.Length)
            {
                _page = pages.Length - 1;
            }

            return pages[_page];
        }

        public int GetPageCount()
        {
            return pages.Length;
        }
    }
}
