using System;
using System.Drawing;
using System.Windows.Forms;

class main{
    static void Main(){
        new handmade.operation_window();
        form fm = new form();
        bool flg = false;

        if(!System.IO.Directory.Exists("log")){
            MessageBox.Show("�ulog�v������܂���B\n�쐬���܂�");
            System.IO.Directory.CreateDirectory("log");
        }

        //�摜����t�@�C���̓����Ă��� pic_folder ���Ȃ��Ƃ��͏I���t���O�𗧂Ă�
        if(!System.IO.Directory.Exists("pic_folder")){
            fm.logwrite_msgbox("error:�upic_folder�v������܂���");
            flg = true;
        }
        //�ݒ�pcsv�t�@�C��������ɓǂݍ��߂Ȃ����Ƃ��ɏI���t���O�𗧂Ă�
        else{
            if(!fm.read_csv())flg = true;
        }

        if(flg){
            fm.logwrite_msgbox("error�̂��ߏI�����܂�");
            return;
        }

        Application.Run(fm);

        fm.logwrite("�A�v���P�[�V�����I��");

    }
}