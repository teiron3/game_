using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

partial class form{
    //���\�b�h supply ��`��ʂ���̕⋋
    //���� b :4bit�ł��ꂼ��̊͑���I������
    //        0b0000 �� 1234 �̏���
    void supply(byte f){
        logwrite("test11");
        //�����`�F�b�N�K���Ȓl�łȂ��ꍇ�̓G���[��Ԃ��ē���I��
        if((f & 15) == 0){
            logwrite_msgbox("error:supply �����G���[");
            stop_flg = true;
            return;
        }

        //delegate ��`��ʂɖ߂�
        Action home_port_return = () =>{
            a_non_b_click("��`_�o��", "��`_��`");
        };

        //deligate �⋋���{
        Action run_supplay = () =>{
            for(int i = 0; i <= 2; i++)a_click("�⋋_�S�⋋");
        };

        //����J�n
        //��`����⋋��ʂɑJ��
        a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return;
        a_non_b_click("�⋋_�R��", "��`_�⋋"); if(stop_flg)return;

        //1�͑��̕⋋
        if((f & 8) != 0){
            run_supplay();
            if(stop_flg)return;
        }

        //2�͑��̕⋋
        if((f & 4) != 0){
            a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��2");
            if(stop_flg)return;

            run_supplay();
            if(stop_flg)return;
        }

        //3�͑��̕⋋
        if((f & 2) != 0){
            a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��3");
            if(stop_flg)return;

            run_supplay();
            if(stop_flg)return;
        }

        //4�͑��̕⋋
        if((f & 1) != 0){
            a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��4");
            if(stop_flg)return;

            run_supplay();
            if(stop_flg)return;
        }

            home_port_return();
            logwrite("�⋋����");
            return;
    }
}