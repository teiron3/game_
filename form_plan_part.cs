using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

partial class form{
    //�⋋
    void supply(byte b){
        //��`��ʂɖ߂�
        Action home_port_return = () =>{
            a_non_b_click("��`_�o��", "���j���[_��`");
        };
        a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return;
        a_non_b_click("��`_�R��", "��`_�⋋"); if(stop_flg)return;

        //�⋋���{
        Action run_supplay = () =>{
            for(int i = 0; i >= 2; i++)a_click("�⋋_�S�⋋");
        };

        //1�͑��̕⋋
        if(b & 8 != 0){
            run_supplay();
            home_port_return(); if(stop_flg)return;
        }

        //2�͑��̕⋋
        if(b & 4 != 0){
            a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��2");
            if(stop_flg)return;

            run_supplay();
            home_port_return(); if(stop_flg)return;
        }


        //3�͑��̕⋋
        if(b & 2 != 0){
            a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��3");
            if(stop_flg)return;

            run_supplay();
            home_port_return(); if(stop_flg)return;
        }

        //4�͑��̕⋋
        if(b & 1 != 0){
            a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��4");
            if(stop_flg)return;

            run_supplay();
            home_port_return(); if(stop_flg)return;
        }
    }
}