
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

partial class form{
    //1-1����
    void around1_1(){
        Fielde1_1();if(stop_flg)return;
        expedition();
    }

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
            expedition();
            Task.Delay(1800000 + rnd(2000000)).Wait();
        }
        logwrite("�����I�����[�v�����I��");
    }
}