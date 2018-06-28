using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

partial class form{
    //画像判定処理用のクラスを宣言しインスタンス化する
    pic_hit p_hit = new pic_hit();

    //マウス操作用のクラスを宣言しインスタンス化する
    mouse_class mouse = new mouse_class();

    //Name に引数 str の値を持つ p_class を返す
    pic_data_class search_class(string str){
        for(int i = 0; i <= this.rows; i++){
            if(p_class[i].Name == str)return p_class[i];
        }
        logwrite_msgbox(str + "は存在しません。csvファイルかコードの確認をしてください。");
        stop_flg = true;
        return null;
    }

    //画像が一致するれば true を返し、一致しなけらば false を返す
    bool pic_con(string str){
        bool flg = p_hit.pic_con(search_class(str));
        if(flg){
            logwrite(str + "の画像が一致しました");
        }else{
            logwrite(str + "の画像は一致しません");
        }
        return flg;
    }

    //画像を発見すれば true を返し、発見できなければ falseを返す
    bool pic_search(string str){
        bool flg = p_hit.pic_search(search_class(str));
        if(flg){
            logwrite(str + "の画像を発見しました");
        }else{
            logwrite(str + "の画像は発見できませんでした");
        }
        return flg;
    }

    //str1 がなければ str2 をクリックする
    //画像が見つからない場合、エラーフラグを立てる
    void a_non_b_click(string str1,string str2){
        pic_data_class tmp_class1,tmp_class2;
        
        tmp_class1 = search_class(str1);
        tmp_class2 = search_class(str2);

        if(tmp_class1 == null || tmp_class2 == null){
            stop_flg = true;
            return;
        }

        for(int cnt = 0; cnt <= 200; cnt++){
            if(p_hit.pic_con(tmp_class1))return;
            mouse.before_move_and_click(tmp_class2);
        }
        logwrite("error:" + tmp_class1.Name + "の画像が見つかりません");
        stop_flg = true;
    }

    //str1 をクリック
    void a_click(string str1){
        pic_data_class tmp_class;
        tmp_class = search_class(str1);
        mouse.before_move_and_click(tmp_class);
    }

    //str1 & str2 の箇所が変化するまで str3 をクリック
    void a_b_change_c_click(string str1, string str2, string str3){

        pic_data_class tmp_class1 = search_class(str1);
        pic_data_class tmp_class2 = search_class(str2);
        p_hit.pic_get(tmp_class1);
        p_hit.pic_get(tmp_class2) ;
        for(int i = 0; i <= 200; i++){
            a_click(str3);
            if(!(pic_con(str1)) || !(pic_con(str2)))return;
        }
        logwrite("error:a_b_change_c_click3 画面が変化しません");
        stop_flg = true;
    }

}