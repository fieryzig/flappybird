local cls_ui_over = class("cls_ui_over",cls_ui_base)
cls_ui_over.s_ui_panel = 'UIPrefabs/OverUI'

function cls_ui_over:ctor()
    self.super.ctor(self)
end

function cls_ui_over:OnStart()
    log("cls_ui_over OnStart")
    
    local _cur_score_label = self.m_transform:FindChild("CurScore").gameObject;
    local _bst_score_label = self.m_transform:FindChild("BstScore").gameObject;
    self.m_play_button = self.m_transform:FindChild("PlayButton").gameObject;

    require "logic/Manager"
    _CurScore,_BstScore = GetScore();
    --if _best_score > _BstScore then _BstScore = _best_score; end;
    -- Current Score
    self.m_cur_score_label = _cur_score_label:GetComponent("UILabel");
    self.m_cur_score_label.text = _CurScore / 2;
    -- Best Score
    self.m_bst_score_label = _bst_score_label:GetComponent("UILabel");
    self.m_bst_score_label.text = _BstScore / 2;
    if GetNetStatus() == 1 then
        require "logic/Network"
        local name = GetUserName();
        send("Over" .. name);
    end
    
    self.m_lua_behaviour:AddClick(self.m_play_button, function()
        self:Close()
        -- Reset
        ResetGame();
    end);
end

function cls_ui_over:OnDestroy()
    self.super.OnDestroy(self)
    log("cls_ui_over OnDestroy")
end

function ShowOverUI()
    cls_ui_over:new()
end
