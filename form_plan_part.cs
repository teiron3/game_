using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

partial class form{
    //�⋋
    void supply(int f){
        //�����`�F�b�N�K���Ȓl�łȂ��ꍇ�̓G���[��Ԃ�
        if(f & 15 = 0){
            logwrite_msgbox("error:�⋋�̈����G���[");
            stop_flg = true;
            return;
        }

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
        if(f & 8 != 0){
            run_supplay();
            home_port_return();
            return;
        }

        //2�͑��̕⋋
        if(f & 4 != 0){
            a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��2");
            if(stop_flg)return;

            run_supplay();
            home_port_return();
            if(stop_flg)return;
        }


        //3�͑��̕⋋
        if(f & 2 != 0){
            a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��3");
            if(stop_flg)return;

            run_supplay();
            home_port_return(); 
            if(stop_flg)return;
        }

        //4�͑��̕⋋
        if(f & 1 != 0){
            a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��4");
            if(stop_flg)return;

            run_supplay();
            home_port_return();
            if(stop_flg)return;
        }

    }
}