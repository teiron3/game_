using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

partial class form{
    //•â‹‹
    void supply(byte b){
        //•ê`‰æ–Ê‚É–ß‚é
        Action home_port_return = () =>{
            a_non_b_click("•ê`_oŒ‚", "ƒƒjƒ…[_•ê`");
        };
        a_non_b_click("•ê`_oŒ‚", "•ê`_•ê`"); if(stop_flg)return;
        a_non_b_click("•ê`_”R—¿", "•ê`_•â‹‹"); if(stop_flg)return;

        //•â‹‹À{
        Action run_supplay = () =>{
            for(int i = 0; i >= 2; i++)a_click("•â‹‹_‘S•â‹‹");
        };

        //1ŠÍ‘à‚Ì•â‹‹
        if(b & 8 != 0){
            run_supplay();
            home_port_return(); if(stop_flg)return;
        }

        //2ŠÍ‘à‚Ì•â‹‹
        if(b & 4 != 0){
            a_b_change_c_click("•â‹‹_”äŠrêŠ1", "•â‹‹_”äŠrêŠ2", "•â‹‹_ŠÍ‘à‘I‘ğ2");
            if(stop_flg)return;

            run_supplay();
            home_port_return(); if(stop_flg)return;
        }


        //3ŠÍ‘à‚Ì•â‹‹
        if(b & 2 != 0){
            a_b_change_c_click("•â‹‹_”äŠrêŠ1", "•â‹‹_”äŠrêŠ2", "•â‹‹_ŠÍ‘à‘I‘ğ3");
            if(stop_flg)return;

            run_supplay();
            home_port_return(); if(stop_flg)return;
        }

        //4ŠÍ‘à‚Ì•â‹‹
        if(b & 1 != 0){
            a_b_change_c_click("•â‹‹_”äŠrêŠ1", "•â‹‹_”äŠrêŠ2", "•â‹‹_ŠÍ‘à‘I‘ğ4");
            if(stop_flg)return;

            run_supplay();
            home_port_return(); if(stop_flg)return;
        }
    }
}