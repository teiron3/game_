using System;
using System.Drawing;
using System.Windows.Forms;

class main{
    static void Main(){
        new handmade.operation_window();
        form fm = new form();
        bool flg = false;

        if(!System.IO.Directory.Exists("log")){
            MessageBox.Show("「log」がありません。\n作成します");
            System.IO.Directory.CreateDirectory("log");
        }

        //画像判定ファイルの入っている pic_folder がないときは終了フラグを立てる
        if(!System.IO.Directory.Exists("pic_folder")){
            fm.logwrite_msgbox("error:「pic_folder」がありません");
            flg = true;
        }
        //設定用csvファイルが正常に読み込めなったときに終了フラグを立てる
        else{
            if(!fm.read_csv())flg = true;
        }

        if(flg){
            fm.logwrite_msgbox("errorのため終了します");
            return;
        }

        Application.Run(fm);

        fm.logwrite("アプリケーション終了");

    }
}