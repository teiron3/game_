
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

partial class form{

    ///<summary>test�p</summary>
    void plantest(){
        kirakan(1);
        expendjitiononly();
    }

    ///<summary>1-1����</summary>
    ///<remarks>1-1 �� ����</remarks>
    void around1_1(){
        Fielde1_1();if(stop_flg)return;
        expedition(true);
    }

    ///<summary>�����̂�</summary>
    ///<remarks>������Ɉ�莞�Ԏ~�܂�</remarks>
    void expendjitiononly(){

        Func<int, int> rnd = (rr) => {
            Random r = new System.Random();
            return r.Next(rr);
        };
        for(int cnt = 0; cnt < 30; cnt++){
            //��`���o����ʂ̑J�ڊm�F�ꏊ
            int portchangeX = 100; int portchangeY = 500;
            //����J�n
            //��`��ʊm�F
            a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return;
            //��`����C��I����ʂɑJ��
            a_non_b_click("�o��_�o��", "��`_�o��", portchangeX, portchangeY); if(stop_flg)return;
            //��`��ʂɖ߂�
            a_non_b_click("��`_�o��", "��`_��`");
            //��������
            expedition(true);
            dockIn();
            Task.Delay(1800000 + rnd(2000000)).Wait();
        }
        logwrite("�����I�����[�v�����I��");
    }

    ///<summary>�L���t��</summary>
    ///<param name="kantai">�͑���</param>
    ///<remarks>�L���t��1-1����</remarks>
    ///<remarks>kantai �͕ۑ�����Ă���͑�������邩�̐�</remarks>
    void kirakan(int kantai){
        //kantai �̐�����(error �h�~)
        kantai = (kantai > 5) ? 5 : kantai;

        //�L���t�����[�v
        //���� kantai �ȉ��̉񐔃��[�v
        for(int kantaicnt = 1; kantaicnt <= kantai; kantaicnt++){
            //kancnt �L���t�����s����
            for(int kancnt = 1; kancnt <= 6; kancnt++){
                
                maketankan(kantaicnt, kancnt);if(stop_flg){return;}
                //�ύX�����͂�����ɏo���Ȃ���Ԃ̎��̓X�L�b�v
                onejudge();if(stop_flg){return;}
                if(continueflg){
                    continueflg = false;
                    continue;
                }
                for(int cnt = 1; cnt <= 3; cnt++){
                    Fielde1_1();if(stop_flg){return;}
                    expedition(true);if(stop_flg){return;}
                }
                dockIn();
            }

        }
    }
}