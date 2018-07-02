using System;
using System.Runtime.InteropServices;
using System.Text;

//�E�B���h�E����N���X
public class operation_windows
{
    IntPtr temp;
    /// <summary>
    /// �G���g���|�C���g
    /// </summary>
    public operation_window()
    {
        //�E�B���h�E��񋓂���
        EnumWindows(new EnumWindowsDelegate(EnumWindowCallBack), IntPtr.Zero);

        Console.ReadLine();
    }

    public delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public extern static bool EnumWindows(EnumWindowsDelegate lpEnumFunc,
        IntPtr lparam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowText(IntPtr hWnd,
        StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetClassName(IntPtr hWnd,
        StringBuilder lpClassName, int nMaxCount);

    private static bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam)
    {
        //�E�B���h�E�̃^�C�g���̒������擾����
        int textLen = GetWindowTextLength(hWnd);
        if (0 < textLen)
        {
            //�E�B���h�E�̃^�C�g�����擾����
            StringBuilder tsb = new StringBuilder(textLen + 1);
            GetWindowText(hWnd, tsb, tsb.Capacity);

            //�E�B���h�E�̃N���X�����擾����
            //StringBuilder csb = new StringBuilder(256);
            //GetClassName(hWnd, csb, csb.Capacity);
            
            if(tsb.ToString().IndexOf("������") >= 0){
                MoveWindow(hWnd, 0, 10, 300, 200, 1);
            }
            //���ʂ�\������
            //Console.WriteLine("�N���X��:" + csb.ToString());
            //Console.WriteLine("�^�C�g��:" + tsb.ToString());
        }

        //���ׂẴE�B���h�E��񋓂���
        return true;
    }

    //�E�B���h�E����p�O�����\�b�h
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int MoveWindow(
        IntPtr hwnd, int x, int y, 
        int nWidth,int nHeight, int bRepaint
    );

}
