using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

partial class form{

    ///<summary>母港画面からの補給</summary>
    ///<param name="oneop">ワンオペ かどうか。損壊判定用</param>
    ///<returns>byte 遠征の判定用</returns>
    ///<returns>bool continuflg</returns>
    ///<remarks>第1艦隊の補給画面で損壊を確認</remarks>
    ///<remarks>第2～4艦隊で補給の有無で戻り値の遠征判定を設定</remarks>
    byte supply(bool oneop){
        //遠征判定用戻り値 byte変数の設定
        byte flg = 0;
        //要補給かどうかの判定場所
        int watchsupplyX = 791;
        int watchsupplyY = 259;

        //delegate 母港画面に戻る
        Action home_port_return = () =>{
            a_non_b_click("母港_出撃", "母港_母港");
        };
        
        Func<bool> color_check = () =>{
            //bitmapクラスのインスタンス化
            Bitmap bitmap = new Bitmap(1,1);

            //ディスプレイの任意の点の取得
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen( new Point(watchsupplyX, watchsupplyY), new Point( 0, 0),  bitmap.Size);
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

        //1艦隊のダメージ確認
        //oneop == true のときは一艦のみ
        //oneope == false のときは6艦見る
        if(oneop){
            continueflg = onedockjudge();

        }else{
            damagejudge();
            dockcheck();
        }

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

    ///<summary>入渠</summary>
    ///<param>なし</param>
    ///<returns>なし</returns>
    ///<remarks>破損している艦を入渠する</remarks>
    ///<remarks>(耐久バーの赤の値が 100 以上)</remarks>
    ///<remarks>dockflg == false のときは抜ける</remarks>
    void dockIn(){
        //ドックフラグがfalse のときは戻る
        if(!dockflg){return;}

        //ドックの空きフラグ
        int dockemp = 0;
        //空きドックの数
        int dockempcnt = 0;
        //入渠ドックの判定場所
        int x = 498;
        int[] y = {270, 392, 516, 638};

        //艦の状態
        int kanx = 919;//耐久バー
        int statasx = 1101;//修復マーク
        int[] kany = {211, 258, 302, 350, 395};

        //入渠ドック入渠判定 red < 60, green > 180, blue > 180
        int red = 60, green = 180, blue = 180;

        //色判定式 → 入渠ドッククリックから判定入渠まで変更
        //指定の箇所に 入渠 の色があればfalseを返す
        //5番目の艦が無傷の時は dockflg = false に
        //上から順に見ていき入渠できる艦を入渠する
        Func<string, bool> docksearch = (str) => {
            string[] tmp = str.Split(',');
            if( (int.Parse(tmp[0]) < red) && (int.Parse(tmp[1]) > green) && (int.Parse(tmp[2]) > blue)){
                return false;
            }
            return true;
        };
        //母港から入渠画面に遷移
        a_non_b_click("母港_出撃", "母港_母港"); if(stop_flg)return ;
        a_non_b_click("入渠_入渠画面", "母港_入渠"); if(stop_flg)return ;

        //ドックの使用判定
        Task<string> task1 = null, task2 = null, task3 = null, task4 = null;
        task1 = Task.Run(() => p_hit.bitcolor(x, y[0]));
        task2 = Task.Run(() => p_hit.bitcolor(x, y[1]));
        task3 = Task.Run(() => p_hit.bitcolor(x, y[2]));
        task4 = Task.Run(() => p_hit.bitcolor(x, y[3]));

        while(!task4.IsCompleted){Task.Delay(200).Wait();}
        while(!task3.IsCompleted){Task.Delay(200).Wait();}
        while(!task2.IsCompleted){Task.Delay(200).Wait();}
        while(!task1.IsCompleted){Task.Delay(200).Wait();}
        
        if(docksearch(task1.Result)){dockemp |= 32;dockempcnt++;}
        if(docksearch(task2.Result)){dockemp |= 16;dockempcnt++;}
        if(docksearch(task3.Result)){dockemp |= 8;dockempcnt++;}
        if(docksearch(task4.Result)){dockemp |= 4;dockempcnt++;}

        //ドックが開いている間かつ dockflg = true のとき入渠処理する
        int bitconst = 32;
        //選択ドック用変数
        int selectdock = 1;
        while((dockempcnt > 0) && dockflg){
            //ドックが開いているとき
            if((dockemp & bitconst) > 0){
                //未入渠艦のカウント
                int cnt = 0;
                //入渠する艦の上からの数
                int kan = 0;
                string[] str = new string[0];
                //艦船選択を開く
                a_non_b_click("入渠_艦船選択", "入渠_ドック" + selectdock.ToString());

                //耐久バーの確認
                //入渠対象の場合 cnt++ 最初の対象艦の順番を kan に入力
                //1st
                if(int.Parse(p_hit.bitcolor(kanx, kany[0]).Split(',')[0]) > 100){
                    if(docksearch(p_hit.bitcolor(statasx, kany[0]))){
                        kan = 1;
                        cnt++;
                    }
                } 
                //2nd
                if(int.Parse(p_hit.bitcolor(kanx, kany[1]).Split(',')[0]) > 100){
                    if(docksearch(p_hit.bitcolor(statasx, kany[1]))){
                        kan = (kan != 0) ? 2: kan;
                        cnt++;
                    }
                } 
                //3rd
                if(int.Parse(p_hit.bitcolor(kanx, kany[2]).Split(',')[0]) > 100){
                    if(docksearch(p_hit.bitcolor(statasx, kany[2]))){
                        kan = (kan != 0) ? 3: kan;
                        cnt++;
                    }
                } 
                //4th
                if(int.Parse(p_hit.bitcolor(kanx, kany[3]).Split(',')[0]) > 100){
                    if(docksearch(p_hit.bitcolor(statasx, kany[3]))){
                        kan = (kan != 0) ? 4: kan;
                        cnt++;
                    }
                } 
                //5th
                //dockflg用
                if(int.Parse(p_hit.bitcolor(kanx, kany[4]).Split(',')[0]) > 100){
                    cnt++;
                } 
                //対象が１以下の場合 dockflg = false
                dockflg = (cnt <= 1) ? false : true;
                //もし入渠が必要な艦がなければ抜ける
                if(cnt == 0){break;}

                //入渠させる
                a_non_b_click("入渠_入渠開始","入渠_艦船" + kan.ToString());
                a_non_b_click("入渠_はい", "入渠_入渠開始");
                a_change_click("入渠_はい");

                Task.Delay(1000).Wait();
            }
            //loop最終処理
            dockempcnt--;
            bitconst >>= 1;
            selectdock++;
        }
        //母港に戻る
        a_non_b_click("母港_出撃", "母港_母港");
    }

    ///<summary>遠征</summary>
    ///<param name="oneop">キラ付け等ワンオペのとき true</param>
    ///<returns>なし</returns>
    ///<remarks>補給して遠征の有無を確認</remarks>
    void expedition(bool oneop){
        //遠征艦隊フラグ
        byte flg = 0;
        //母港から出撃画面に遷移するときの画像比較場所
        int portchangeX = 580;
        int portchangeY = 500;

        //補給し、遠征艦隊の確認
        flg = supply(oneop);
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
        a_non_b_click("出撃_出撃", "母港_出撃", portchangeX, portchangeY); if(stop_flg)return;
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
                System.Threading.Tasks.Task.Delay(10000).Wait();
                a_change_click(tmp_str2, tmp_str1);
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

    ///<summary>1-1出撃</summary>
    ///<remarks>1回出撃</remarks>
    void Fielde1_1(){
        //母港→出撃画面の遷移確認場所
        int portchangeX = 580; int portchangeY = 500;
        //出撃画面→海域画面の遷移確認場所
        int seaX = 275; int seaY = 583;
        //海域画面→海域決定の遷移確認場所
        int seadicisionX = 900; int seadicisionY = 600;
        //海域決定→艦隊決定の遷移確認場所
        int fleeddicisionX = 715; int fleeddicisionY = 633;
        //動作開始
        //母港画面確認
        a_non_b_click("母港_出撃", "母港_母港"); if(stop_flg)return;
        //母港から海域選択画面に遷移
        a_non_b_click("出撃_出撃", "母港_出撃", portchangeX, portchangeY); if(stop_flg)return;
        a_non_b_click("出撃_海域画面", "出撃_出撃", seaX, seaY); if(stop_flg)return;
        a_non_b_click("出撃決定_海域決定", "出撃海域詳細_1", seadicisionX, seadicisionY); if(stop_flg)return;
        a_non_b_click("出撃決定_出撃決定", "出撃決定_海域決定", fleeddicisionX, fleeddicisionY); if(stop_flg)return;
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

    ///<summary>耐久の確認(母港→補給)</summary>
    ///<returns>bool continueflg に入力</returns>
    ///<remarks>単艦で艦の周回継続を確認する</remarks>
    ///<remarks>主に変更直後に使用</remarks>
    void onejudge(){
        //動作開始
        //母港から補給画面に遷移
        a_non_b_click("母港_出撃", "母港_母港"); if(stop_flg)return ;
        a_non_b_click("補給_燃料", "母港_補給"); if(stop_flg)return ;
        //艦の確認
        continueflg = onedockjudge();
        
        //母港に戻る
        a_non_b_click("母港_出撃", "母港_母港");
    }
}