using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace TodoList
{
    public class Task
    {
        #region Private members

        /// <summary>
        /// An unique identification number of the task
        /// </summary>
        private int _id;

        #endregion


        #region Public properties

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Complete { get; set; }

        public int ID { get; set; }

        #endregion


        #region Public methods

        #endregion


        #region Constructors

        public Task()
        {

        }

        #endregion
    }
}
