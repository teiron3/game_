using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

partial class form{

    //���\�b�h supply ��`��ʂ���̕⋋
    //�߂�l�͉����̔���p
    byte supply(){
        //��������p�߂�l byte�ϐ��̐ݒ�
        byte flg = 0;

        //delegate ��`��ʂɖ߂�
        Action home_port_return = () =>{
            a_non_b_click("��`_�o��", "��`_��`");
        };
        
        Func<bool> color_check = () =>{
            //bitmap�N���X�̃C���X�^���X��
            Bitmap bitmap = new Bitmap(1,1);

            //�f�B�X�v���C�̔C�ӂ̓_�̎擾
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen( new Point(525, 225), new Point( 0, 0),  bitmap.Size);
            g.Dispose();

            //Color�N���X�̃C���X�^���X��
            Color c = bitmap.GetPixel(0,0);
            if(c.B >= 5){
                return true;
            }else{
                return false;
            }
        };

        //deligate �⋋���{
        Action run_supplay = () =>{
            for(int i = 0; i <= 2; i++)a_click("�⋋_�S�⋋");
        };

        //����J�n
        //��`����⋋��ʂɑJ��
        a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return flg;
        a_non_b_click("�⋋_�R��", "��`_�⋋"); if(stop_flg)return flg;

        //1�͑��̕⋋
        if(color_check()){ run_supplay();}

        //2�͑��̕⋋
        a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��2");
        if(stop_flg)return flg;
        if(color_check()){
            run_supplay();
            logwrite("2in");
            flg |= 4;
        }
        if(stop_flg)return flg;

        //3�͑��̕⋋
        a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��3");
        if(stop_flg)return flg;
        if(color_check()){
            run_supplay();
            logwrite("3in");
            flg |= 2;
        }
        //4�͑��̕⋋
        a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��4");
        if(stop_flg)return flg;
        if(color_check()){
            run_supplay();
            logwrite("4in");
            flg |= 1;
        }

        home_port_return();
        logwrite("�⋋����");
        return flg;
    }

    //���� �` test now
    void expedition(){
        //�����͑��t���O
        byte flg = 0;

        //�⋋���A�����͑��̊m�F
        flg = supply();
        //�����͑�����`�ɖ߂��ĂȂ������甲����
        if(flg == 0)return;

        //�ݒ�t�@�C���̗L���̊m�F
        if(!System.IO.File.Exists(@"expandition.ini")) {
            logwrite("error:�����ݒ�t�@�C��������܂���");
            stop_flg = true;
            return;
        }
        //�����ݒ�t�@�C���̓ǂݍ���
        byte[] expandition_data = new byte[6];
        FileStream stream = File.Open(@".\expandition.ini", FileMode.Open, FileAccess.Read);
        stream.Read(expandition_data, 0, 6);
        stream.Close();
        
        //������ʂ�
        //��`��ʊm�F
        a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return;
        //��`����o����ʂɑJ��
        a_non_b_click("�o��_�o��", "��`_�o��", 195, 415); if(stop_flg)return;
        //������ʂɑJ��
        a_non_b_click("����_���","�o��_����"); if(stop_flg)return;

        //�����͑�����O�܂Ői�߂郁�\�b�h
        //�ϐ� i �͊͑� �ϐ� b �͑O�̊͑��̊C�� �߂�l�͏������s�����C��
        Func<int, int, int> sub_expendition = (i, b) => {
            //error process
            if((expandition_data[i] <= 0) || (expandition_data[i + 1] >= 9)){
                stop_flg = true;
                return 0;
            }
            string tmp_str1 = "�����C��_" + expandition_data[i].ToString();
            string tmp_str2 = "�����C��ڍ�_" + expandition_data[i + 1].ToString();
            if(expandition_data[i] != b){
                a_change_click(tmp_str1);
            }
            a_non_b_click("��������_�C�挈��", tmp_str2); 
            a_change_click("��������_�C�挈��");
            //a_non_b_click("��������_�����J�n", "��������_�C�挈��"); 

            return expandition_data[i];
        };

        //��������܂ŏ�������
        //�O�̊͑��̊C�� �����l��1
        int bf = 1;
        //��2�͑��̏���
        if((flg & 4) > 0){
            bf = sub_expendition(0, bf);if(stop_flg)return;
            a_non_b_click("����_���", "��������_�����J�n");if(stop_flg)return;
            logwrite("��2�͑������o��");
        }
        //��3�͑��̏���
        if((flg & 2) > 0){
            bf = sub_expendition(2, bf);if(stop_flg)return;
            a_change_click("��������_�͑��I��3");if(stop_flg)return;
            a_non_b_click("����_���", "��������_�����J�n");if(stop_flg)return;
            logwrite("��3�͑������o��");
        }
        //��4�͑��̏���
        if((flg & 1) > 0){
            bf = sub_expendition(4, bf);if(stop_flg)return;
            a_change_click("��������_�͑��I��4");if(stop_flg)return;
            a_non_b_click("����_���", "��������_�����J�n");if(stop_flg)return;
            logwrite("��4�͑������o��");
        }

        //��`��ʂɖ߂�
        a_non_b_click("��`_�o��", "��`_��`");
    }

    //1-1�o��
    void Fielde1_1(){
        //����J�n
        //��`��ʊm�F
        a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return;
        //��`����C��I����ʂɑJ��
        a_non_b_click("�o��_�o��", "��`_�o��", 195, 415); if(stop_flg)return;
        a_non_b_click("�o���C��_1", "�o��_�o��", 350, 435); if(stop_flg)return;
        a_non_b_click("�o������_�C�挈��", "�o���C��ڍ�_1", 456, 166); if(stop_flg)return;
        a_non_b_click("�o������_�o������", "�o������_�C�挈��", 618, 256); if(stop_flg)return;
        a_b_change_c_click("�o������_��r�ꏊ1","�o������_��r�ꏊ2","�o������_�o������");if(stop_flg)return;

        //debug cnt
        int dcnt = 0;
        //�C��퓬
        do{
            System.Threading.Thread.Sleep(200);
            while(pic_con("�퓬_���j��")){
                a_click("��`_��`");
                System.Threading.Thread.Sleep(800);
            }
            if(pic_con("�퓬_�I��")){
                while(!pic_con("�퓬_�i��")){
                    a_click("��`_��`");
                    System.Threading.Thread.Sleep(800);
                    if(pic_con("��`_�o��")){
                        return;
                    }
                }
                a_del_a_click("�퓬_�i��");
            }

            if(pic_con("�퓬_�i��")){
                logwrite("�i��");
                a_del_a_click("�퓬_�i��");
            }
            if(pic_con("�퓬_���")){
                a_del_a_click("�퓬_���");
            }

            if(stop_flg)return;
            logwrite(dcnt.ToString());
            dcnt++;
        }while(!pic_con("��`_�o��"));
    }
}