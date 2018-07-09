using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

partial class form{
    //�摜���菈���p�̃N���X��錾���C���X�^���X������
    pic_hit p_hit = new pic_hit();

    //�}�E�X����p�̃N���X��錾���C���X�^���X������
    mouse_class mouse = new mouse_class();

    //�摜����v������ true ��Ԃ��A��v���Ȃ���� false ��Ԃ�
    bool pic_con(string str){
        bool flg = p_hit.pic_con(p_class[str]);
        if(flg){
            logwrite(str + "�̉摜����v���܂���");
        }else{
            logwrite(str + "�̉摜�͈�v���܂���");
        }
        return flg;
    }

    //�摜�𔭌������ true ��Ԃ��A�����ł��Ȃ���� false��Ԃ�
    bool pic_search(string str){
        bool flg = p_hit.pic_search(p_class[str]);
        if(flg){
            logwrite(str + "�̉摜�𔭌����܂���");
        }else{
            logwrite(str + "�̉摜�͔����ł��܂���ł���");
        }
        return flg;
    }

    void a_click(string str){
        mouse.before_move_and_click(p_class[str]);
    }
    //str1 ���Ȃ���� str2 ���N���b�N����
    //�摜��������Ȃ��ꍇ�A�G���[�t���O�𗧂Ă�
    void a_non_b_click(string str1,string str2){
        
        if(p_class[str1] == null || p_class[str2] == null){
            stop_flg = true;
            return;
        }

        for(int cnt = 0; cnt <= 200; cnt++){
            if(p_hit.pic_con(p_class[str1]))return;
            mouse.before_move_and_click(p_class[str2]);
        }
        logwrite("error:" + p_class[str1].Name + "�̉摜��������܂���");
        stop_flg = true;
    }

    //str1 & str2 �̉ӏ����ω�����܂� str3 ���N���b�N
    void a_b_change_c_click(string str1, string str2, string str3){
        p_hit.pic_get(p_class[str1]);
        p_hit.pic_get(p_class[str2]) ;
        for(int i = 0; i <= 200; i++){
            mouse.before_move_and_click(p_class[str3]);
            if(!(pic_con(str1)) || !(pic_con(str2)))return;
        }
        logwrite("error:a_b_change_c_click3 ��ʂ��ω����܂���");
        stop_flg = true;
    }

}