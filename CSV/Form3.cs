using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            TopMost = true;
            Activate();
            InitializeComponent();
        }

        private const int WH_KEYBOARD_LL = 13;//Keyboard hook;

        //Keys data structure
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
        }

        //Using callbacks
        private LowLevelKeyboardProcDelegate m_callback;
        private LowLevelKeyboardProcDelegate m_callback_1;
        private LowLevelKeyboardProcDelegate m_callback_2;
        private LowLevelKeyboardProcDelegate m_callback_3;
        private LowLevelKeyboardProcDelegate m_callback_4;
        private LowLevelKeyboardProcDelegate m_callback_5;
        private LowLevelKeyboardProcDelegate m_callback_6;
        private LowLevelKeyboardProcDelegate m_callback_7;

        //переменные для разблокировки клавиатуры
        private IntPtr m_hHook;
        private IntPtr m_hHook_1;
        private IntPtr m_hHook_2;
        private IntPtr m_hHook_3;
        private IntPtr m_hHook_4;
        private IntPtr m_hHook_5;
        private IntPtr m_hHook_6;
        private IntPtr m_hHook_7;

        //Установка перехвата клавиатуры
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, IntPtr hMod, int dwThreadId);

        //Разблокировка клавиатуры
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //Hook handle
        [System.Runtime.InteropServices.DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

        //Вызов следующего хука
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        //Захват <WinKey> 
        public IntPtr LowLevelKeyboardHookProc_win(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                 (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.RWin ||
                    objKeyInfo.key == Keys.LWin)
                {
                    return (IntPtr)1;//<WinKey> 
                }
            }
            return CallNextHookEx(m_hHook_1, nCode, wParam, lParam);
        }

        //Захват <Delete> 
        public IntPtr LowLevelKeyboardHookProc_del(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {

                KBDLLHOOKSTRUCT objKeyInfo =
                 (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.Delete)
                {
                    return (IntPtr)1;//<Delete> 
                }
            }
            return CallNextHookEx(m_hHook_3, nCode, wParam, lParam);
        }

        //Захват <Control> 
        public IntPtr LowLevelKeyboardHookProc_ctrl(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                    (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.RControlKey ||
                    objKeyInfo.key == Keys.LControlKey)
                {
                    return (IntPtr)1;//<Control> 
                }
            }
            return CallNextHookEx(m_hHook_2, nCode, wParam, lParam);
        }

        //Захват <Alt> 
        public IntPtr LowLevelKeyboardHookProc_alt(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {

                KBDLLHOOKSTRUCT objKeyInfo =
                 (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.Alt)
                {
                    return (IntPtr)1;//<Alt> 
                }
            }
            return CallNextHookEx(m_hHook_4, nCode, wParam, lParam);
        }

        //Блокировка <Alt>+<Tab> 
        public IntPtr LowLevelKeyboardHookProc_alt_tab(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                 (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.Alt ||
                    objKeyInfo.key == Keys.Tab)
                {
                    return (IntPtr)1;//<Alt>+<Tab> blocking
                }
            }
            return CallNextHookEx(m_hHook, nCode, wParam, lParam);
        }

        //Блокировка <Alt>+<Space> 
        public IntPtr LowLevelKeyboardHookProc_alt_space(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                    (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.Alt ||
                    objKeyInfo.key == Keys.Space)
                {
                    return (IntPtr)1;//<Alt>+<Space> 
                }
            }
            return CallNextHookEx(m_hHook_5, nCode, wParam, lParam);
        }

        //Блокировка <Control>+<Shift>+<Escape> 
        public IntPtr LowLevelKeyboardHookProc_control_shift_escape(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                 (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.LControlKey ||
                    objKeyInfo.key == Keys.RControlKey ||
                    objKeyInfo.key == Keys.LShiftKey ||
                    objKeyInfo.key == Keys.RShiftKey ||
                    objKeyInfo.key == Keys.Escape)
                {
                    return (IntPtr)1;//<Control>+<Shift>+<Escape> 
                }
            }
            return CallNextHookEx(m_hHook_6, nCode, wParam, lParam);//Go to next hook
        }

        //Блокировка <Control>+<Alt>+<Del> 
        public IntPtr LowLevelKeyboardHookProc_control_alt_del(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                 (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.LControlKey ||
                    //objKeyInfo.key == Keys.RControlKey || 
                    //objKeyInfo.key == Keys.Control || 
                    //objKeyInfo.key == Keys.ControlKey || 
                    objKeyInfo.key == Keys.Alt ||
                    //objKeyInfo.key == Keys.LMenu || 
                    //objKeyInfo.key == Keys.Menu || 
                    //objKeyInfo.key == Keys.RMenu || 
                    objKeyInfo.key == Keys.Delete)
                {
                    return (IntPtr)1;//<Control>+<Alt>+<Del> 
                }
            }
            return CallNextHookEx(m_hHook_7, nCode, wParam, lParam);
        }


        //Delegate for using hooks
        private delegate IntPtr LowLevelKeyboardProcDelegate(int nCode, IntPtr wParam, IntPtr lParam);

        //Настройки хука
        public void SetHook()
        {
            //Hooks callbacks by delegate
            m_callback = LowLevelKeyboardHookProc_win;
            m_callback_1 = LowLevelKeyboardHookProc_alt_tab;
            m_callback_2 = LowLevelKeyboardHookProc_ctrl;
            m_callback_3 = LowLevelKeyboardHookProc_del;
            m_callback_4 = LowLevelKeyboardHookProc_alt;
            m_callback_5 = LowLevelKeyboardHookProc_alt_space;
            m_callback_6 = LowLevelKeyboardHookProc_control_shift_escape;
            m_callback_7 = LowLevelKeyboardHookProc_control_alt_del;

            //Настройки перехвата
            m_hHook = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_1 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_1, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_2 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_2, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_3 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_3, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_4 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_4, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_5 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_5, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_6 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_6, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_7 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_7, GetModuleHandle(IntPtr.Zero), 0);
        }

        //Блокировка сочитаний группой
        public void Unhook()
        {
            UnhookWindowsHookEx(m_hHook);
            UnhookWindowsHookEx(m_hHook_1);
            UnhookWindowsHookEx(m_hHook_2);
            UnhookWindowsHookEx(m_hHook_3);
            UnhookWindowsHookEx(m_hHook_4);
            UnhookWindowsHookEx(m_hHook_5);
            UnhookWindowsHookEx(m_hHook_6);
            UnhookWindowsHookEx(m_hHook_7);
        }

        //Блокировка <Alt>+<Tab> 
        private void button1_Click(object sender, EventArgs e)
        {
            m_callback_1 = LowLevelKeyboardHookProc_alt_tab;
            m_hHook_1 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_1, GetModuleHandle(IntPtr.Zero), 0);
        }
        //Разблокировка <Alt>+<Tab> 
        private void button10_Click(object sender, EventArgs e)
        {
            UnhookWindowsHookEx(m_hHook_1);
        }

        //Блокировка <Alt>+<Space> 
        private void button5_Click(object sender, EventArgs e)
        {
            m_callback_5 = LowLevelKeyboardHookProc_alt_space;
            m_hHook_5 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_5, GetModuleHandle(IntPtr.Zero), 0);
        }

        //Разблокировка <Alt>+<Space> 
        private void button6_Click(object sender, EventArgs e)
        {
            UnhookWindowsHookEx(m_hHook_5);
        }

        //Блокировка <Control>+<Shift>+<Escape> 
        private void button2_Click(object sender, EventArgs e)
        {
            m_callback_6 = LowLevelKeyboardHookProc_control_shift_escape;
            m_hHook_6 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_6, GetModuleHandle(IntPtr.Zero), 0);
        }

        //Разблокировка <Control>+<Shift>+<Escape> 
        private void button9_Click(object sender, EventArgs e)
        {
            UnhookWindowsHookEx(m_hHook_6);
        }

        //Блокировка <Control>+<Alt>+<Del> 
        private void button4_Click(object sender, EventArgs e)
        {
            m_callback_7 = LowLevelKeyboardHookProc_control_alt_del;
            m_hHook_7 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_7, GetModuleHandle(IntPtr.Zero), 0);
        }

        //Разблокировка <Control>+<Alt>+<Del> 
        private void button7_Click(object sender, EventArgs e)
        {
            UnhookWindowsHookEx(m_hHook_7);
        }

        //Блокировка <WinKey> 
        private void button3_Click(object sender, EventArgs e)
        {
            m_callback = LowLevelKeyboardHookProc_win;
            m_hHook = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback, GetModuleHandle(IntPtr.Zero), 0);
        }

        //Разблокировка <WinKey> 
        private void button8_Click(object sender, EventArgs e)
        {
            UnhookWindowsHookEx(m_hHook);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                FormCollection F = Application.OpenForms;//получили коллекцию всех открытых форм
                F[0].FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                //F[0].WindowState = System.Windows.Forms.FormWindowState.Maximized;
                //F[0].StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                FormCollection F = Application.OpenForms;//получили коллекцию всех открытых форм
                F[0].FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            }

            if (checkBox2.Checked)
            {
                FormCollection F = Application.OpenForms;//получили коллекцию всех открытых форм
                //F[0].FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                F[0].WindowState = System.Windows.Forms.FormWindowState.Maximized;
                //F[0].StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                FormCollection F = Application.OpenForms;//получили коллекцию всех открытых форм
                F[0].WindowState = System.Windows.Forms.FormWindowState.Normal;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
