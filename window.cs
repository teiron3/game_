using System;
using System.Runtime.InteropServices;
using System.Text;

//�E�B���h�E����N���X
namespace handmade{
    
public class operation_window{
    IntPtr temp;
    /// <summary>
    /// �G���g���|�C���g
    /// </summary>
    public operation_window()
    {
        //�E�B���h�E��񋓂���
        EnumWindows(new EnumWindowsDelegate(EnumWindowCallBack), IntPtr.Zero);
        Console.WriteLine("test");
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

    //�E�B���h�E����p�O�����\�b�h
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int MoveWindow(
        IntPtr hwnd, int x, int y, 
        int nWidth,int nHeight, int bRepaint
    );

    private static bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam)
    {
        //�E�B���h�E�̃^�C�g���̒������擾����
        int textLen = GetWindowTextLength(hWnd);
        if (0 < textLen) {
            //�E�B���h�E�̃^�C�g�����擾����
            StringBuilder tsb = new StringBuilder(textLen + 1);
            GetWindowText(hWnd, tsb, tsb.Capacity);

            //���ʂ�\������
            if((tsb.ToString().IndexOf("�͂���") >= 0) && (tsb.ToString().IndexOf("Edge") >= 0)){
                //�E�B���h�E��ݒ肷��
                MoveWindow(hWnd, 0, -150, 1240, 900, 1);
                //�^�C�g���ƃn���h�����R���\�[���ɕ\��
                //Console.WriteLine("�^�C�g��:{0} handle:{1}" ,tsb.ToString(),textLen);

            }
        }

        //���ׂẴE�B���h�E��񋓂���
        return true;
    }
}
}