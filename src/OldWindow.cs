using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AstrOrzPlayer
{
    public class OldWindow : System.Windows.Forms.IWin32Window
    {
        IntPtr _handle;
        public OldWindow(IntPtr handle)
        {
            _handle = handle;
        }
        #region IWin32Window Members
        IntPtr System.Windows.Forms.IWin32Window.Handle
        {
            get { return _handle; }
        }
        #endregion
    }
}
