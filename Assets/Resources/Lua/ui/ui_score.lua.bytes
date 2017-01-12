local cls_ui_score = class("cls_ui_score",cls_ui_base)
cls_ui_score.s_ui_panel = 'UIPrefabs/ScoreUI'
local _ui_score_instance = nil


function cls_ui_score:ctor()
    self.super.ctor(self);
end

function cls_ui_score:OnStart()
    local _score_label = self.m_transform:FindChild("Score").gameObject;
    self.m_score_label = _score_label:GetComponent("UILabel");
--[[
    local _progress = self.m_transform:FindChild("Progress").gameObject;
    self.m_progress = _progress:GetComponent("UISlider");
    self.m_value = self.m_progress.value;
    self.m_ability_on = false;

    self.m_fire_button = self.m_transform:FindChild("FireButton").gameObject;
    self.m_lua_behaviour:AddClick(self.m_fire_button,function(obj)
        if self.m_progress.value == 1 then
            require "logic/Manager"
            StartAbility();
            self.m_ability_on = true;
        end
    end);
    self.m_pre_time = 0;
]]--
    self:EnableUpdate();
end

function cls_ui_score:Update()
    require "logic/Manager"
    _CurScore,_BstScore = GetScore();
    self.m_score_label.text = _CurScore / 2;
--[[    
    local _time = Time.realtimeSinceStartup;
    local deltime = _time - self.m_pre_time;

    if self.m_ability_on and self.m_value > 0 and deltime > 0.2 then
        self.m_value = self.m_value - 0.1;
        self.m_pre_time = _time;
    end
    if self.m_ability_on == false and self.m_value < 1 and deltime > 1 then
        self.m_value = self.m_value + 0.1;
        self.m_pre_time = _time;
    end
    self.m_progress.value = self.m_value;
    -- log(self.m_value);
    if self.m_value <= 0 then
        CloseAbility();
        self.m_ability_on = false;
        self.m_value = 0;
    end 
]]-- 
end

function cls_ui_score:OnDestroy()
    self.super.OnDestroy(self)
    log("cls_ui_score OnDestroy")
end

function ShowScoreUI()
    _ui_score_instance = cls_ui_score:new()
end

function DisableScoreUI()
    _ui_score_instance:Close()
end