using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

partial class form{
    //�摜���菈���p�̃N���X��錾���C���X�^���X������
    pic_hit p_hit = new pic_hit();

    //�}�E�X����p�̃N���X��錾���C���X�^���X������
    mouse_class mouse = new mouse_class();

    //Name �Ɉ��� str �̒l������ p_class ��Ԃ�
    pic_data_class search_class(string str){
        for(int i = 0; i <= this.rows; i++){
            if(p_class[i].Name == str)return p_class[i];
        }
        logwrite_msgbox(str + "�͑��݂��܂���Bcsv�t�@�C�����R�[�h�̊m�F�����Ă��������B");
        stop_flg = true;
        return null;
    }

    //�摜����v������ true ��Ԃ��A��v���Ȃ���� false ��Ԃ�
    bool pic_con(string str){
        bool flg = p_hit.pic_con(search_class(str));
        if(flg){
            logwrite(str + "�̉摜����v���܂���");
        }else{
            logwrite(str + "�̉摜�͈�v���܂���")
        }
        return flg;
    }

    //�摜�𔭌������ true ��Ԃ��A�����ł��Ȃ���� false��Ԃ�
    bool pic_search(string str){
        bool flg = p_hit.pic_search(search_class(str));
        if(flg){
            logwrite(str + "�̉摜�𔭌����܂���");
        }else{
            logwrite(str + "�̉摜�͔����ł��܂���ł���")
        }
        return flg;
    }

    void a_non_b_click(string str1,string str2){
        pic_data_class tmp_class1,tmp_class2;
        
        tmp_class1 = search_class(str1);
        tmp_class2 = search_class(str2);

        if(tmp_class1 == null || tmp_class2 == null){
            stop_flg = true;
            return;
        }

        for(int cnt = 0; cnt <= 1000; cnt++){
            if(p_hit.pic_con(tmp_class1))return;
            mouse.before_move_and_click(tmp_class2);
        }
        stop_flg = true;
    }


}