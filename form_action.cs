using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

partial class form{
    //画像判定処理用のクラスを宣言しインスタンス化する
    pic_hit p_hit = new pic_hit();

    //マウス操作用のクラスを宣言しインスタンス化する
    mouse_class mouse = new mouse_class();

    //画像が一致するれば true を返し、一致しなけらば false を返す
    bool pic_con(string str){
        bool flg = p_hit.pic_con(p_class[str]);
        /*if(flg){
            logwrite(str + "の画像が一致しました");
        }else{
            logwrite(str + "の画像は一致しません");
        }*/
        return flg;
    }

    //画像を発見すれば true を返し、発見できなければ falseを返す
    bool pic_search(string str){
        bool flg = p_hit.pic_search(p_class[str]);
        /*if(flg){
            logwrite(str + "の画像を発見しました");
        }else{
            logwrite(str + "の画像は発見できませんでした");
        }*/
        return flg;
    }

    void a_click(string str){
        mouse.before_move_and_click(p_class[str]);
    }

    //画像が消えるまでクリック
    void a_del_a_click(string str){
        if(p_class[str] == null){
            stop_flg = true;
            return;
        }

        while(p_hit.pic_con(p_class[str])){
            mouse.before_move_and_click(p_class[str]);
            mouse.move_cursor(p_class["母港_母港"]);
            System.Threading.Thread.Sleep(1500);
        }
    }
    //str1 がなければ str2 をクリックする
    //画像が見つからない場合、エラーフラグを立てる
    void a_non_b_click(string str1,string str2){
        
        if(p_class[str1] == null || p_class[str2] == null){
            stop_flg = true;
            return;
        }

        System.Threading.Thread.Sleep(300);

        if(pic_con(str1)) return;

        //変化比較用画像取得
        pic_data_class cmpclass = new pic_data_class();
        cmpclass.Pic_X = p_class[str1].Pic_X;
        cmpclass.Pic_Y = p_class[str1].Pic_Y;
        cmpclass.Pic_Width = p_class[str1].Pic_Width;
        cmpclass.Pic_Height = p_class[str1].Pic_Height;
        p_hit.pic_get(cmpclass);

        //screen comp flg set
        bool flg = true;
        for(int ccnt = 0; ccnt < 30; ccnt++){
            for(int cnt = 0; cnt <= 20; cnt++){
                if(stop_flg)return;

                if(flg){
                    a_click(str2);
                    mouse.move_cursor(p_class["母港_母港"]);
                }

                if(flg){
                    for(int subcnt = 0; flg && subcnt <= 5 ; subcnt++){
                        System.Threading.Thread.Sleep(400);
                        flg = p_hit.pic_con(cmpclass);
                    }
                }
                if(pic_con(str1)) return;
            }
            flg = true;
        }
        logwrite("error:" + p_class[str1].Name + "の画像が見つかりません");
        stop_flg = true;
    }

    //オーバーロード(画面の一部変更検知有) 
    void a_non_b_click(string str1,string str2,int cmp_x,int cmp_y){
        //変化比較用画像取得
        pic_data_class cmpclass = new pic_data_class();
        cmpclass.Pic_X = cmp_x;
        cmpclass.Pic_Y = cmp_y;
        cmpclass.Pic_Width = 10;
        cmpclass.Pic_Height = 10;
        p_hit.pic_get(cmpclass);

        //screen comp flg set
        bool flg = true;
        for(int cnt = 0; cnt <= 30; cnt++){
            if(stop_flg)return;
            if(flg){
                a_click(str2);
                mouse.move_cursor(p_class["母港_母港"]);
            }

            if(flg){
                for(int subcnt = 0; flg && subcnt <= 5 ; subcnt++){
                    System.Threading.Thread.Sleep(600);
                    flg = p_hit.pic_con(cmpclass);
                }
            }
            if(!flg){
                if(pic_con(str1)){
                     break;
                }
            }
        }
    }

    //オーバーロード str1 の箇所が変化するまで str1 をクリック
    void a_change_click(string str1){
        if(p_class[str1] == null ){
            stop_flg = true;
            return;
        }

        Task.Delay(300).Wait();

        //変化比較用画像取得
        pic_data_class cmpclass = new pic_data_class();
        cmpclass.Pic_X = p_class[str1].X;
        cmpclass.Pic_Y = p_class[str1].Y;
        cmpclass.Pic_Width = p_class[str1].Width;
        cmpclass.Pic_Height = p_class[str1].Height;
        p_hit.pic_get(cmpclass);

        //screen comp flg set
        bool flg = true;

        for(int cnt = 0; cnt <= 30; cnt++){
            if(stop_flg)return;

            if(flg){
                a_click(str1);
                mouse.move_cursor(p_class["母港_母港"]);
            }

            if(flg){
                for(int subcnt = 0; flg && subcnt <= 5 ; subcnt++){
                    Task.Delay(400).Wait();
                    flg = p_hit.pic_con(cmpclass);
                }
            }else{
                return;
            }
        }
        logwrite("error:" + p_class[str1].Name + "の画像が変化しません");
        stop_flg = true;
    }

    //オーバーロード x, y の箇所が変化するまで str1 をクリック
    void a_change_click(string str1, int x, int y){
        if(p_class[str1] == null ){
            stop_flg = true;
            return;
        }

        System.Threading.Thread.Sleep(300);

        //変化比較用画像取得
        pic_data_class cmpclass = new pic_data_class();
        cmpclass.Pic_X = x;
        cmpclass.Pic_Y = y;
        cmpclass.Pic_Width = 10;
        cmpclass.Pic_Height = 10;
        p_hit.pic_get(cmpclass);

        //screen comp flg set
        bool flg = true;

        for(int cnt = 0; cnt <= 30; cnt++){
            if(stop_flg)return;

            if(flg){
                a_click(str1);
                mouse.move_cursor(p_class["母港_母港"]);
            }

            if(flg){
                for(int subcnt = 0; flg && subcnt <= 5 ; subcnt++){
                    System.Threading.Thread.Sleep(400);
                    flg = p_hit.pic_con(cmpclass);
                }
            }else{
                return;
            }
        }
        logwrite("error:" + p_class[str1].Name + "の画像が変化しません");
        stop_flg = true;
    }

    //str1 の箇所が変化するまで str2 をクリック
    void a_change_click(string str1, string str2){
        if(p_class[str1] == null || p_class[str2] == null){
            stop_flg = true;
            return;
        }

        System.Threading.Thread.Sleep(300);


        //変化比較用画像取得
        pic_data_class cmpclass = new pic_data_class();
        cmpclass.Pic_X = p_class[str1].X;
        cmpclass.Pic_Y = p_class[str1].Y;
        cmpclass.Pic_Width = p_class[str1].Width;
        cmpclass.Pic_Height = p_class[str1].Height;
        p_hit.pic_get(cmpclass);

        //screen comp flg set
        bool flg = true;

        for(int cnt = 0; cnt <= 30; cnt++){
            if(stop_flg)return;

            if(flg){
                a_click(str2);
                mouse.move_cursor(p_class["母港_母港"]);
            }

            if(flg){
                for(int subcnt = 0; flg && subcnt <= 5 ; subcnt++){
                    System.Threading.Thread.Sleep(600);
                    flg = p_hit.pic_con(cmpclass);
                }
            }else{
                return;
            }
        }
        logwrite("error:" + p_class[str1].Name + "の画像が変化しません");
        stop_flg = true;
    }
    
    //str1 & str2 の箇所が変化するまで str3 をクリック
    void a_b_change_c_click(string str1, string str2, string str3){
        p_hit.pic_get(p_class[str1]);
        p_hit.pic_get(p_class[str2]) ;
        for(int i = 0; i <= 20; i++){
            mouse.before_move_and_click(p_class[str3]);
            if(!(pic_con(str1)) || !(pic_con(str2)))return;
        }
        logwrite("error:a_b_change_c_click3 画面が変化しません");
        stop_flg = true;
    }

    //破損状況の判定(補給画面)
    //ダメージ判定用フラグ int damageRed,damageOrange
    void damagejudge(){
        //ダメージ判定フラグの初期化
        damageRed = 0;
        damageOrange = 0;
        //ダメージ判定用の色数値
        //red 大破時の緑の値(<100)
        //orange 中破以上の赤の値(=>250)
        //blue 正常な青の値、違う場合は大破判定(>100)
        int red = 100, orange = 250,blue = 100;
        //耐久判定用の座標(耐久バー)
        int x = 488;
        int[] y = {272, 350, 427, 504, 581, 657};
        //艦の数判定用の座標と色(ドラム缶)
        int kanx = 713;
        int[] kany = {252, 330, 405, 483, 559, 635};
        //艦の数をカウントする変数
        int cnt = 0;
        //返り値の色数値を代入する変数
        string[] tmp = new string[3];
        //艦の耐久色数値Get
        //TVask<string> task = new Task<int, int, string>;
        //task[0] = new Task<int, int,string>( (x, y[0]) => p_hit.bitcolor30(x, y));
        //task[1] = new Task<int, int,string>( (x, y[1]) => p_hit.bitcolor30(x, y));

        
        for(; cnt <= 5; cnt++){
            tmp = p_hit.bitcolor(kanx, kany[cnt]).Split(',');
            //cntに艦がなければ判定が終了するまで待って判定用数値をGet
            if(int.Parse(tmp[2]) >= 150){
                break;
            }
        }
        //艦の耐久判定をする
        Action<int, Task<string>> taskcomplete = (bittmp, taskx) =>{
                    while(!taskx.IsCompleted)Task.Delay(1000).Wait();
                    tmp = taskx.Result.Split(',');
                    logwrite(taskx.Result);
                    if(int.Parse(tmp[0]) >= orange)damageOrange |= bittmp;
                    if(int.Parse(tmp[1]) < red || int.Parse(tmp[2]) > blue)damageRed |= bittmp;
        };
        Task<string> task1, task2, task3, task4, task5, task6;
        task1 = Task.Run(() => p_hit.bitcolor30(x, y[0]));
        logwrite("1");
        if(cnt > 1){
             task2 = Task.Run( () => p_hit.bitcolor30(x, y[1]));
        logwrite("2");
            if(cnt > 2){
                task3 = Task.Run( () => p_hit.bitcolor30(x, y[2]));
        logwrite("3");
                if(cnt > 3){
                    task4 = Task.Run( () => p_hit.bitcolor30(x, y[3]));
        logwrite("4");
                    if(cnt > 4){
                        task5 = Task.Run( () => p_hit.bitcolor30(x, y[4]));
        logwrite("5");
                        if(cnt > 5){
                            task6 = Task.Run( () => p_hit.bitcolor30(x, y[5]));
        logwrite("6");
                            taskcomplete(1, task6);
                        }
                        taskcomplete(2, task5);
                    }
                    taskcomplete(4, task4);
                }
                taskcomplete(8, task3);
            }
            taskcomplete(16, task2);
        }
        taskcomplete(32, task1);

        if(damageRed > 0)logwrite("大破艦あり");
        if(damageOrange > 0)logwrite("中破以上の艦あり");
        logwrite("end");

    }

    void maketankan(int groupnum, int kannum){
        //母港確認
        a_non_b_click("母港_出撃", "母港_母港"); if(stop_flg)return ;
        //母港→編成画面
        a_non_b_click("編成_画面", "母港_編成"); if(stop_flg)return ;
        //編成展開画面へ
        

    }

}