using System;
using System.Runtime.InteropServices;
using System.Text;

//ウィンドウ操作クラス
public class operation_windows
{
    IntPtr temp;
    /// <summary>
    /// エントリポイント
    /// </summary>
    public operation_window()
    {
        //ウィンドウを列挙する
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
        //ウィンドウのタイトルの長さを取得する
        int textLen = GetWindowTextLength(hWnd);
        if (0 < textLen)
        {
            //ウィンドウのタイトルを取得する
            StringBuilder tsb = new StringBuilder(textLen + 1);
            GetWindowText(hWnd, tsb, tsb.Capacity);

            //ウィンドウのクラス名を取得する
            //StringBuilder csb = new StringBuilder(256);
            //GetClassName(hWnd, csb, csb.Capacity);
            
            if(tsb.ToString().IndexOf("メモ帳") >= 0){
                MoveWindow(hWnd, 0, 10, 300, 200, 1);
            }
            //結果を表示する
            //Console.WriteLine("クラス名:" + csb.ToString());
            //Console.WriteLine("タイトル:" + tsb.ToString());
        }

        //すべてのウィンドウを列挙する
        return true;
    }

    //ウィンドウ操作用外部メソッド
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int MoveWindow(
        IntPtr hwnd, int x, int y, 
        int nWidth,int nHeight, int bRepaint
    );

}
