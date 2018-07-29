using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

partial class form{

    //•â‹‹ƒtƒ‰ƒO
    byte supplyFlg = 0;

    //ƒƒ\ƒbƒh supply •ê`‰æ–Ê‚©‚ç‚Ì•â‹‹
    //ˆø” b :4bit‚Å‚»‚ê‚¼‚ê‚ÌŠÍ‘à‚ð‘I‘ð‚·‚é
    //        0b0000 ‚Å 1234 ‚Ì‡”Ô
    void supply(byte f){
        //ˆø”ƒ`ƒFƒbƒN“K³‚È’l‚Å‚È‚¢ê‡‚ÍƒGƒ‰[‚ð•Ô‚µ‚Ä“®ìI—¹
        if((f & 15) == 0){
            logwrite_msgbox("error:supply ˆø”ƒGƒ‰[");
            stop_flg = true;
            return;
        }

        //delegate •ê`‰æ–Ê‚É–ß‚é
        Action home_port_return = () =>{
            a_non_b_click("•ê`_oŒ‚", "•ê`_•ê`");
        };

        //deligate •â‹‹ŽÀŽ{
        Action run_supplay = () =>{
            for(int i = 0; i <= 2; i++)a_click("•â‹‹_‘S•â‹‹");
        };

        //“®ìŠJŽn
        //•ê`‚©‚ç•â‹‹‰æ–Ê‚É‘JˆÚ
        a_non_b_click("•ê`_oŒ‚", "•ê`_•ê`"); if(stop_flg)return;
        a_non_b_click("•â‹‹_”R—¿", "•ê`_•â‹‹"); if(stop_flg)return;

        //1ŠÍ‘à‚Ì•â‹‹
        if((f & 8) != 0){
            run_supplay();
            if(stop_flg)return;
        }

        //2ŠÍ‘à‚Ì•â‹‹
        if((f & 4) != 0){
            a_b_change_c_click("•â‹‹_”äŠrêŠ1", "•â‹‹_”äŠrêŠ2", "•â‹‹_ŠÍ‘à‘I‘ð2");
            if(stop_flg)return;

            run_supplay();
            if(stop_flg)return;
        }

        //3ŠÍ‘à‚Ì•â‹‹
        if((f & 2) != 0){
            a_b_change_c_click("•â‹‹_”äŠrêŠ1", "•â‹‹_”äŠrêŠ2", "•â‹‹_ŠÍ‘à‘I‘ð3");
            if(stop_flg)return;

            run_supplay();
            if(stop_flg)return;
        }

        //4ŠÍ‘à‚Ì•â‹‹
        if((f & 1) != 0){
            a_b_change_c_click("•â‹‹_”äŠrêŠ1", "•â‹‹_”äŠrêŠ2", "•â‹‹_ŠÍ‘à‘I‘ð4");
            if(stop_flg)return;

            run_supplay();
            if(stop_flg)return;
        }

            home_port_return();
            logwrite("•â‹‹Š®—¹");
            return;
    }

    //‰“ª ` making now
    void expedition(){
        //‰“ªŠÍ‘àƒtƒ‰ƒO
        byte flg = 0;

        //•ê`‰æ–ÊŠm”F
        a_non_b_click("•ê`_oŒ‚", "•ê`_•ê`"); if(stop_flg)return;
        //•Ò¬‰æ–Ê‚É‘JˆÚ
        a_non_b_click("•Ò¬_‰æ–Ê", "•ê`_•Ò¬"); if(stop_flg)return;
        
    }

    //1-1oŒ‚
    void Fielde1_1(){
        //“®ìŠJŽn
        //•ê`‰æ–ÊŠm”F
        a_non_b_click("•ê`_oŒ‚", "•ê`_•ê`"); if(stop_flg)return;
        //•ê`‚©‚çŠCˆæ‘I‘ð‰æ–Ê‚É‘JˆÚ
        a_non_b_click("oŒ‚_oŒ‚", "•ê`_oŒ‚"); if(stop_flg)return;
        a_non_b_click("oŒ‚ŠCˆæ_1", "oŒ‚_oŒ‚"); if(stop_flg)return;
        a_non_b_click("oŒ‚Œˆ’è_ŠCˆæŒˆ’è", "oŒ‚ŠCˆæÚ×_1"); if(stop_flg)return;
        a_non_b_click("oŒ‚Œˆ’è_oŒ‚Œˆ’è", "oŒ‚Œˆ’è_ŠCˆæŒˆ’è"); if(stop_flg)return;
        a_b_change_c_click("oŒ‚Œˆ’è_”äŠrêŠ1","oŒ‚Œˆ’è_”äŠrêŠ2","oŒ‚Œˆ’è_oŒ‚Œˆ’è");if(stop_flg)return;

        //ŠCˆæí“¬
        do{
            System.Threading.Thread.Sleep(1000);
            a_click("•ê`_•ê`");
            if(pic_con("í“¬_iŒ‚"))a_del_a_click("í“¬_iŒ‚");
            if(pic_con("í“¬_–éí"))a_del_a_click("í“¬_–éí");
            if(stop_flg)return;

        }while(pic_con("•ê`_oŒ‚"));
        supplyFlg |= 8;
    }
}