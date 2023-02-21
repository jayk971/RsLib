using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
namespace RsLib.Common
{
    public sealed class PinnedObject : IDisposable
    {
        #region Field

        private GCHandle _handle;      // Garbage collector handle

        #endregion

        #region Property

        /// <summary>
        /// Get the address.
        /// </summary>
        public IntPtr Pointer
        {
            // Get the leading address of the current object that is pinned.
            get { return _handle.AddrOfPinnedObject(); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="target">Target to protect from the garbage collector</param>
        public PinnedObject(object target)
        {
            // Pin the target to protect it from the garbage collector.
            _handle = GCHandle.Alloc(target, GCHandleType.Pinned);
        }

        #endregion

        #region Interface
        /// <summary>
        /// Interface
        /// </summary>
        public void Dispose()
        {
            _handle.Free();
            _handle = new GCHandle();
        }

        #endregion
    }

}
