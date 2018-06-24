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
            logwrite(str + "の画像は一致しません")
        }
        return flg;
    }

    //画像を発見すれば true を返し、発見できなければ falseを返す
    bool pic_search(string str){
        bool flg = p_hit.pic_search(search_class(str));
        if(flg){
            logwrite(str + "の画像を発見しました");
        }else{
            logwrite(str + "の画像は発見できませんでした")
        }
        return flg;
    }

    void a_non_b_click(string str1,string str2){
        pic_data_class tmp_class1,tmp_class2;
        
        tmp_class1 = search_class(str1);
        tmp_class2 = search_class(str2);

        if(tmp_class1 == null || tmp_class2 == null){
            stop_flg = true;
            return;
        }

        for(int cnt = 0; cnt <= 1000; cnt++){
            if(p_hit.pic_con(tmp_class1))return;
            mouse.before_move_and_click(tmp_class2);
        }
        stop_flg = true;
    }


}