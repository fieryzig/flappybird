local cls_ui_start = class("cls_ui_start",cls_ui_base)
cls_ui_start.s_ui_panel = 'UIPrefabs/BeginUI'

function cls_ui_start:ctor()
    self.super.ctor(self)
end

function cls_ui_start:OnStart()
    log("cls_ui_start OnStart")
    self.m_play_button = self.m_transform:FindChild("PlayButton").gameObject;
    self.m_lua_behaviour:AddClick(self.m_play_button, function (obj)
        --require "logic/Manager"
        --StartGame();
        --self:Close();
        require "ui/ui_login"
        ShowLoginUI();
        self:Close();
    end);
end

function cls_ui_start:OnDestroy()
    self.super.OnDestroy(self)
    log("cls_ui_start OnDestroy")
end


function ShowStartUI()
    cls_ui_start:new()
end