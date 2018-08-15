using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

partial class form{

    //メソッド supply 母港画面からの補給
    //戻り値は遠征の判定用
    byte supply(){
        //遠征判定用戻り値 byte変数の設定
        byte flg = 0;

        //delegate 母港画面に戻る
        Action home_port_return = () =>{
            a_non_b_click("母港_出撃", "母港_母港");
        };
        
        Func<bool> color_check = () =>{
            //bitmapクラスのインスタンス化
            Bitmap bitmap = new Bitmap(1,1);

            //ディスプレイの任意の点の取得
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen( new Point(525, 225), new Point( 0, 0),  bitmap.Size);
            g.Dispose();

            //Colorクラスのインスタンス化
            Color c = bitmap.GetPixel(0,0);
            if(c.B >= 5){
                return true;
            }else{
                return false;
            }
        };

        //deligate 補給実施
        Action run_supplay = () =>{
            for(int i = 0; i <= 2; i++)a_click("補給_全補給");
        };

        //動作開始
        //母港から補給画面に遷移
        a_non_b_click("母港_出撃", "母港_母港"); if(stop_flg)return flg;
        a_non_b_click("補給_燃料", "母港_補給"); if(stop_flg)return flg;

        //1艦隊の補給
        if(color_check()){ run_supplay();}

        //2艦隊の補給
        a_b_change_c_click("補給_比較場所1", "補給_比較場所2", "補給_艦隊選択2");
        if(stop_flg)return flg;
        if(color_check()){
            run_supplay();
            logwrite("2in");
            flg |= 4;
        }
        if(stop_flg)return flg;

        //3艦隊の補給
        a_b_change_c_click("補給_比較場所1", "補給_比較場所2", "補給_艦隊選択3");
        if(stop_flg)return flg;
        if(color_check()){
            run_supplay();
            logwrite("3in");
            flg |= 2;
        }
        //4艦隊の補給
        a_b_change_c_click("補給_比較場所1", "補給_比較場所2", "補給_艦隊選択4");
        if(stop_flg)return flg;
        if(color_check()){
            run_supplay();
            logwrite("4in");
            flg |= 1;
        }

        home_port_return();
        logwrite("補給完了");
        return flg;
    }

    //遠征 ～ test now
    void expedition(){
        //遠征艦隊フラグ
        byte flg = 0;

        //補給し、遠征艦隊の確認
        flg = supply();
        //遠征艦隊が母港に戻ってなかったら抜ける
        if(flg == 0)return;

        //設定ファイルの有無の確認
        if(!System.IO.File.Exists(@"expandition.ini")) {
            logwrite("error:遠征設定ファイルがありません");
            stop_flg = true;
            return;
        }
        //遠征設定ファイルの読み込み
        byte[] expandition_data = new byte[6];
        FileStream stream = File.Open(@".\expandition.ini", FileMode.Open, FileAccess.Read);
        stream.Read(expandition_data, 0, 6);
        stream.Close();
        
        //遠征画面へ
        //母港画面確認
        a_non_b_click("母港_出撃", "母港_母港"); if(stop_flg)return;
        //母港から出撃画面に遷移
        a_non_b_click("出撃_出撃", "母港_出撃", 195, 415); if(stop_flg)return;
        //遠征画面に遷移
        a_non_b_click("遠征_画面","出撃_遠征"); if(stop_flg)return;

        //遠征艦隊決定前まで進めるメソッド
        //変数 i は艦隊 変数 b は前の艦隊の海域 戻り値は処理を行った海域
        Func<int, int, int> sub_expendition = (i, b) => {
            //error process
            if((expandition_data[i] <= 0) || (expandition_data[i + 1] >= 9)){
                stop_flg = true;
                return 0;
            }
            string tmp_str1 = "遠征海域_" + expandition_data[i].ToString();
            string tmp_str2 = "遠征海域詳細_" + expandition_data[i + 1].ToString();
            if(expandition_data[i] != b){
                a_change_click(tmp_str1);
            }
            a_non_b_click("遠征決定_海域決定", tmp_str2); 
            a_change_click("遠征決定_海域決定");
            //a_non_b_click("遠征決定_遠征開始", "遠征決定_海域決定"); 

            return expandition_data[i];
        };

        //遠征決定まで処理する
        //前の艦隊の海域 初期値は1
        int bf = 1;
        //第2艦隊の処理
        if((flg & 4) > 0){
            bf = sub_expendition(0, bf);if(stop_flg)return;
            a_non_b_click("遠征_画面", "遠征決定_遠征開始");if(stop_flg)return;
            logwrite("第2艦隊遠征出発");
        }
        //第3艦隊の処理
        if((flg & 2) > 0){
            bf = sub_expendition(2, bf);if(stop_flg)return;
            a_change_click("遠征決定_艦隊選択3");if(stop_flg)return;
            a_non_b_click("遠征_画面", "遠征決定_遠征開始");if(stop_flg)return;
            logwrite("第3艦隊遠征出発");
        }
        //第4艦隊の処理
        if((flg & 1) > 0){
            bf = sub_expendition(4, bf);if(stop_flg)return;
            a_change_click("遠征決定_艦隊選択4");if(stop_flg)return;
            a_non_b_click("遠征_画面", "遠征決定_遠征開始");if(stop_flg)return;
            logwrite("第4艦隊遠征出発");
        }

        //母港画面に戻る
        a_non_b_click("母港_出撃", "母港_母港");
    }

    //1-1出撃
    void Fielde1_1(){
        //動作開始
        //母港画面確認
        a_non_b_click("母港_出撃", "母港_母港"); if(stop_flg)return;
        //母港から海域選択画面に遷移
        a_non_b_click("出撃_出撃", "母港_出撃", 195, 415); if(stop_flg)return;
        a_non_b_click("出撃海域_1", "出撃_出撃", 350, 435); if(stop_flg)return;
        a_non_b_click("出撃決定_海域決定", "出撃海域詳細_1", 456, 166); if(stop_flg)return;
        a_non_b_click("出撃決定_出撃決定", "出撃決定_海域決定", 618, 256); if(stop_flg)return;
        a_b_change_c_click("出撃決定_比較場所1","出撃決定_比較場所2","出撃決定_出撃決定");if(stop_flg)return;

        //debug cnt
        int dcnt = 0;
        //海域戦闘
        do{
            System.Threading.Thread.Sleep(200);
            while(pic_con("戦闘_羅針盤")){
                a_click("母港_母港");
                System.Threading.Thread.Sleep(800);
            }
            if(pic_con("戦闘_終了")){
                while(!pic_con("戦闘_進撃")){
                    a_click("母港_母港");
                    System.Threading.Thread.Sleep(800);
                    if(pic_con("母港_出撃")){
                        return;
                    }
                }
                a_del_a_click("戦闘_進撃");
            }

            if(pic_con("戦闘_進撃")){
                logwrite("進撃");
                a_del_a_click("戦闘_進撃");
            }
            if(pic_con("戦闘_夜戦")){
                a_del_a_click("戦闘_夜戦");
            }

            if(stop_flg)return;
            logwrite(dcnt.ToString());
            dcnt++;
        }while(!pic_con("母港_出撃"));
    }
}