using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

partial class form{
    //画像判定処理用のクラスを宣言しインスタンス
    pic_hit p_hit = new pic_hit();

    //マウス操作用のクラスを宣言しインスタンス化する
    mouse_class mouse = new mouse_class();

    ///<summary>画像の判定</summary>
    ///<param name="str">csvファイルから読み込んだ判定用クラス</param>
    ///<returns>bool 画像が一致すれば true</returns>
    ///<remarks>str の位置の画像が一致すれば true を返し、一致しなければ false を返す</remarks>
    bool pic_con(string str){
        bool flg = p_hit.pic_con(p_class[str]);
        /*if(flg){
            logwrite(str + "の画像が一致しました");
        }else{
            logwrite(str + "の画像は一致しません");
        }*/
        return flg;
    }

    ///<summary>画面内の画像の判定</summary>
    ///<param name="str">csvファイルから読み込んだ判定用クラス</param>
    ///<remarks>str の画像を発見すれば true を返し、発見できなければ falseを返す</remarks>
    ///<remarks>改造の余地あり。使っていない</remarks>
    bool pic_search(string str){
        bool flg = p_hit.pic_search(p_class[str]);
        /*if(flg){
            logwrite(str + "の画像を発見しました");
        }else{
            logwrite(str + "の画像は発見できませんでした");
        }*/
        return flg;
    }

    ///<summary>一回クリック</summary>
    ///<param name="str">csvファイルから読み込んだ判定用クラス</param>
    ///<returns>なし</returns>
    ///<remarks>str の位置を一回クリック</remarks>
    void a_click(string str){
        mouse.before_move_and_click(p_class[str]);
    }

    ///<summary> str 画像が消えるまで str をクリック </summary>
    ///<param name="str">csvファイルから読み込んだ判定用クラス</param>
    ///<returns>なし</returns>
    ///<remarks>strの画像が消えるまでstrをクリックする</remarks>
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

    ///<summary> str1 がなければ str2 をクリックする </summary>
    ///<param name="str1">画像の判定用クラス</param>
    ///<param name="str2">クリックする場所の判定用クラス</param>
    ///<remarks>画像が見つからない場合、エラーフラグを立てる</remarks>
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

    ///<summary> str1 がなければ str2 をクリックする 一部場所チェック付 </summary>
    ///<param name="str1">画像の判定用クラス</param>
    ///<param name="str2">クリックする場所の判定用クラス</param>
    ///<param name="cmp_x">画像の判定場所 X座標</param>
    ///<param name="cmp_y">画像の判定場所 y座標</param>
    ///<remarks>str1 の画像がなければ str2 をクリック</remarks>
    ///<remarks>cmp_x,cmp_yの箇所が変化しなければディレイをかける</remarks>
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

    ///<summary> str1 の箇所が変化するまで str1 をクリック </summary>
    ///<param name="str1">判定用クラス</param>
    ///<remarks>str1 の箇所が変化するまで str1 をクリック</remarks>
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

    ///<summary> x, y の箇所(10 * 10 pixel)が変化するまで str1 をクリック </summary>
    ///<param name="str1">クリックする場所の判定用クラス</param>
    ///<param name="x">画像の判定場所 X座標</param>
    ///<param name="y">画像の判定場所 y座標</param>
    ///<remarks>x,y の箇所が変化するまで str1 をクリック</remarks>
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

    ///<summary> str1 の箇所が変化するまで str2 をクリック </summary>
    ///<param name="str1">クリックする場所の判定用クラス</param>
    ///<param name="str2">判定する場所の判定用クラス x,yを使用</param>
    ///<remarks>str2.x .y の箇所が変化するまで str1 をクリック</remarks>
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
    
    ///<summary> str1 と str2 の箇所が変化するまで str3 をクリック </summary>
    ///<param name="str1">判定する場所の判定用クラス picx,yを使用</param>
    ///<param name="str2">判定する場所の判定用クラス picx,yを使用</param>
    ///<param name="str3">クリックする場所の判定用クラス</param>
    ///<remarks>str1 と str2 の箇所が変化するまで str3 をクリック</remarks>
    ///<remarks>str1 と str2 の箇所の画像を取得して判定</remarks>
    ///<remarks>x,yを使用するように改造の余地あり</remarks>
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

    ///<summary>破損状況の判定(補給画面)</summary>
    ///<param>なし</param>
    ///<returns>int damageRed と int damageOrenge に結果を入力。戻り値はなし</returns>
    ///<remarks>補給画面でダメージを判定する</remarks>
    ///<remarks>選択中の艦隊の耐久バーの色を判定</remarks>
    ///<remarks>判定は煙を考えて数回</remarks>
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
        if(cnt > 1){
             task2 = Task.Run( () => p_hit.bitcolor30(x, y[1]));
            if(cnt > 2){
                task3 = Task.Run( () => p_hit.bitcolor30(x, y[2]));
                if(cnt > 3){
                    task4 = Task.Run( () => p_hit.bitcolor30(x, y[3]));
                    if(cnt > 4){
                        task5 = Task.Run( () => p_hit.bitcolor30(x, y[4]));
                        if(cnt > 5){
                            task6 = Task.Run( () => p_hit.bitcolor30(x, y[5]));
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
    }

    ///<summary>入渠チェック(補給)</summary>
    ///<param>なし</param>
    ///<returns>bool dockflg に結果を入力</returns>
    ///<remarks>ダメージ判定用フラグ int damageRed,damageOrange を使用</remarks>
    ///<remarks>入渠の必要な艦があれば dockflg を true にする</remarks>
    void dockcheck(){
        if(damageOrange == 0){return;}
        //入渠中の判定設定色(red < 60, green >180, blue > 190)
        int red = 60, green = 180, blue = 190;
        //判定の場所
        int x = 454;
        int[] y = {277, 351, 429, 507, 585, 661};

        Task<string> task1 = null, task2 = null, task3 = null, task4 = null, task5 = null, task6 = null;
        if((damageOrange & 32) > 0){
            task1 = Task.Run(() => p_hit.bitcolor30(x, y[0]));
        }
        if((damageOrange & 16) > 0){
            task2 = Task.Run(() => p_hit.bitcolor30(x, y[1]));
        }
        if((damageOrange & 8) > 0){
            task3 = Task.Run(() => p_hit.bitcolor30(x, y[2]));
        }
        if((damageOrange & 4) > 0){
            task4 = Task.Run(() => p_hit.bitcolor30(x, y[3]));
        }
        if((damageOrange & 2) > 0){
            task5 = Task.Run(() => p_hit.bitcolor30(x, y[4]));
        }
        if((damageOrange & 1) > 0){
            task6 = Task.Run(() => p_hit.bitcolor30(x, y[5]));
        }
        //艦が入渠中か判定する
        //入渠中でなければ dockflg = true
        Action<Task<string>> taskcomplete = (taskx) =>{
                    while(!taskx.IsCompleted)Task.Delay(1000).Wait();
                    string[] tmp = taskx.Result.Split(',');
                    logwrite(taskx.Result);
                    if(int.Parse(tmp[0]) > red && int.Parse(tmp[1]) < green && int.Parse(tmp[2]) < blue){
                        dockflg = true;
                    } 
        };
        if((damageOrange & 32) > 0){
            while(!task1.IsCompleted){Task.Delay(500).Wait();}
            taskcomplete(task1);
        }
        if((damageOrange & 16) > 0){
            while(!task2.IsCompleted){Task.Delay(500).Wait();}
            taskcomplete(task2);
        }
        if((damageOrange & 8) > 0){
            while(!task3.IsCompleted){Task.Delay(500).Wait();}
            taskcomplete(task3);
        }
        if((damageOrange & 4) > 0){
            while(!task4.IsCompleted){Task.Delay(500).Wait();}
            taskcomplete(task4);
        }
        if((damageOrange & 2) > 0){
            while(!task5.IsCompleted){Task.Delay(500).Wait();}
            taskcomplete(task5);
        }
        if((damageOrange & 1) > 0){
            while(!task6.IsCompleted){Task.Delay(500).Wait();}
            taskcomplete(task6);
        }
    }

    ///<summary>キラ付け単艦の耐久チェック(補給)</summary>
    ///<param>なし</param>
    ///<returns>bool 艦に入渠処置が必要であるなら true</returns>
    ///<remarks>入渠していたとき、中破以上のとき true を返す</remarks>
    ///<remarks>ダメージがあれば dockflg を true に</remarks>
    bool onedockjudge(){
        //耐久バーの座標
        int x = 488;
        int y = 272;
        //入渠中判定用の座標
        int dockx = 454;
        int docky = 277;
        //耐久バーの色取得
        string[] tmp = p_hit.bitcolor(x, y).Split(',');
        //破損があれば dockflg = true
        if(int.Parse(tmp[0]) >= 100){
            dockflg = true;
        }
        //中破判定
        if((int.Parse(tmp[1]) < 200) ||(int.Parse(tmp[2]) > 100)){
            return true;
        }
        //入渠判定
        tmp = p_hit.bitcolor(dockx, docky).Split(',');
        if((int.Parse(tmp[0]) < 60) & (int.Parse(tmp[1]) > 180) & (int.Parse(tmp[2]) > 180)){
            return true;
        }

        return false;
    }

    ///<summary>単艦設定</summary>
    ///<param name="groupnum">艦隊</param>
    ///<param name="kannum">艦</param>
    ///<returns>なし</returns>
    ///<remarks>艦を単艦配置する</remarks>
    void maketankan(int groupnum, int kannum){
        //外す艦のカウント用変数
        int tmp = kannum;
        //母港確認
        a_non_b_click("母港_出撃", "母港_母港"); if(stop_flg)return ;
        //母港→編成画面
        a_non_b_click("編成_画面", "母港_編成"); if(stop_flg)return ;
        //編成展開画面へ
        a_change_click("編成_編成展開", 193, 641);
        //編成展開
        a_change_click("編成展開_展開" + groupnum.ToString(), 197, 215);

        while(tmp > 1){
            a_non_b_click("編成_艦船選択", "編成_変更(旗艦)");
            a_change_click("編成_艦船選択", "編成_外す");
            tmp--;
        }

        a_non_b_click("編成_空の第2艦", "編成_旗艦以外外す");

        //母港に戻る
        a_non_b_click("母港_出撃", "母港_母港"); if(stop_flg)return ;

    }

}