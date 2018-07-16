using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

partial class form{
    //メソッド supply 母港画面からの補給
    //引数 b :4bitでそれぞれの艦隊を選択する
    //        0b0000 で 1234 の順番
    void supply(byte f){
        logwrite("test11");
        //引数チェック適正な値でない場合はエラーを返して動作終了
        if((f & 15) == 0){
            logwrite_msgbox("error:supply 引数エラー");
            stop_flg = true;
            return;
        }

        //delegate 母港画面に戻る
        Action home_port_return = () =>{
            a_non_b_click("母港_出撃", "母港_母港");
        };

        //deligate 補給実施
        Action run_supplay = () =>{
            for(int i = 0; i <= 2; i++)a_click("補給_全補給");
        };

        //動作開始
        //母港から補給画面に遷移
        a_non_b_click("母港_出撃", "母港_母港"); if(stop_flg)return;
        a_non_b_click("補給_燃料", "母港_補給"); if(stop_flg)return;

        //1艦隊の補給
        if((f & 8) != 0){
            run_supplay();
            if(stop_flg)return;
        }

        //2艦隊の補給
        if((f & 4) != 0){
            a_b_change_c_click("補給_比較場所1", "補給_比較場所2", "補給_艦隊選択2");
            if(stop_flg)return;

            run_supplay();
            if(stop_flg)return;
        }

        //3艦隊の補給
        if((f & 2) != 0){
            a_b_change_c_click("補給_比較場所1", "補給_比較場所2", "補給_艦隊選択3");
            if(stop_flg)return;

            run_supplay();
            if(stop_flg)return;
        }

        //4艦隊の補給
        if((f & 1) != 0){
            a_b_change_c_click("補給_比較場所1", "補給_比較場所2", "補給_艦隊選択4");
            if(stop_flg)return;

            run_supplay();
            if(stop_flg)return;
        }

            home_port_return();
            logwrite("補給完了");
            return;
    }
}