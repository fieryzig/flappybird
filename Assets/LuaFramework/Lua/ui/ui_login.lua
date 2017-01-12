local cls_ui_login = class("cls_ui_login",cls_ui_base)
cls_ui_login.s_ui_panel = 'UIPrefabs/LoginUI'

function cls_ui_login:ctor()
    self.super.ctor(self)
end

function cls_ui_login:OnStart()
    log("cls_ui_login OnStart")
    self.m_play_button = self.m_transform:FindChild("PlayButton").gameObject;
    self.m_local_button = self.m_transform:FindChild("LocalButton").gameObject;
    local _hint_label = self.m_transform:FindChild("Hint").gameObject;
    self.m_hint = _hint_label:GetComponent("UILabel");
    local username_input = self.m_transform:FindChild("Username").gameObject;
    local username = username_input:GetComponent("UIInput");
    self.m_input = username.label;
    self.username = self.m_input.text;
    self.m_lua_behaviour:AddClick(self.m_play_button, function(obj)
        require "logic/Network"
        
        status = connect();
        if status == -1 then
            self.m_hint.text = "无法连接服务器，请重试"
            return
        end
        require "logic/Manager"
        SetNetStatus(1);

        self.username = self.m_input.text;
        if self.username ~= "" then

            if string.find(self.username,";") ~= nil then
                self.m_hint.text = "用户名非法"
            else
            
                send("Login" .. self.username);
                log("Login" .. self.username);
                SetUserName(self.username);
                --msg = recv();
                --log(msg);
                
                --_best_score = msg;
                -- SetBestScore(tonumber(msg)*2);
                StartGame();
                self:Close();
            end
        else
            self.m_hint.text = "用户名不能为空"
        end
    end);
    self.m_lua_behaviour:AddClick(self.m_local_button, function(obj)
        require "logic/Manager"
        SetNetStatus(2);
        StartGame();
        self:Close();
    end);

end

function cls_ui_login:OnDestroy()
    self.super.OnDestroy(self)
    log("cls_ui_login OnDestroy");
end

function ShowLoginUI()
    cls_ui_login:new();
end