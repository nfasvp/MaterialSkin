using System;
using System.Windows.Forms;

using MaterialSkin;

public class MouseWheelRedirector : IMessageFilter
{
    private static MouseWheelRedirector instance = null;
    private static bool _active = false;

    public static bool Active
    {
        set
        {
            if (_active != value)
            {
                _active = value;
                if (_active)
                {
                    if (instance == null)
                        instance = new MouseWheelRedirector();
                    Application.AddMessageFilter(instance);
                }
                else if (instance != null)
                    Application.RemoveMessageFilter(instance);
            }
        }
        get
        {
            return _active;
        }
    }

    public static void Attach(Control control)
    {
        if (!_active)
            Active = true;
        control.MouseEnter += instance.ControlMouseEnter;
        control.MouseLeave += instance.ControlMouseLeaveOrDisposed;
        control.Disposed += instance.ControlMouseLeaveOrDisposed;
    }

    public static void Detach(Control control)
    {
        if (instance == null)
            return;
        control.MouseEnter -= instance.ControlMouseEnter;
        control.MouseLeave -= instance.ControlMouseLeaveOrDisposed;
        control.Disposed -= instance.ControlMouseLeaveOrDisposed;
        if (instance.currentControl == control)
            instance.currentControl = null;
    }

    public MouseWheelRedirector()
    {
    }

    private Control currentControl;

    private void ControlMouseEnter(object sender, System.EventArgs e)
    {
        var control = (Control)sender;
        if (!control.Focused)
            currentControl = control;
        else
            currentControl = null;
    }

    private void ControlMouseLeaveOrDisposed(object sender, System.EventArgs e)
    {
        if (currentControl == sender)
            currentControl = null;
    }

    public bool PreFilterMessage(ref System.Windows.Forms.Message m)
    {
        if (currentControl != null && m.Msg == NativeWin.WM_MOUSEWHEEL)
        {
            NativeWin.SendMessage(currentControl.Handle, m.Msg, m.WParam, m.LParam);
            return true;
        }
        else
            return false;
    }

}
