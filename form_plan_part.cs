using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

partial class form{

    //���\�b�h supply ��`��ʂ���̕⋋
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
            g.CopyFromScreen( new Point(100, 100), new Point( 0, 0),  bitmap.Size);
            g.Dispose();

            //Color�N���X�̃C���X�^���X��
            Color c = bitmap.GetPixel(0,0);
            if(c.B <= 5){
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
            flg |= 4;
        }
        if(stop_flg)return flg;

        //3�͑��̕⋋
        a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��3");
        if(stop_flg)return flg;
        if(color_check()){
            run_supplay();
            flg |= 2;
        }
        //4�͑��̕⋋
        a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��4");
        if(stop_flg)return flg;
        if(color_check()){
            run_supplay();
            flg |= 1;
        }

            home_port_return();
            logwrite("�⋋����");
            return flg;
    }

    //���� �` making now
    void expedition(){
        //�����͑��t���O
        byte flg = 0;

        //�⋋���A�����͑��̊m�F
        flg = supply();
        //�����͑�����`�ɖ߂��ĂȂ������甲����
        if(flg == 0)return;
        logwrite(flg.ToString());
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
        logwrite("�o���C��I��");
        a_non_b_click("�o������_�o������", "�o������_�C�挈��", 618, 256); if(stop_flg)return;
        logwrite("�o���C�挈��");
        a_b_change_c_click("�o������_��r�ꏊ1","�o������_��r�ꏊ2","�o������_�o������");if(stop_flg)return;
        logwrite("�o��");

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