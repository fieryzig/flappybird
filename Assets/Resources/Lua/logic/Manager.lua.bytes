_best_score = 0
_curr_score = 0

function StartGame()
    LuaHelper.StartGame();
    SendGlobalMessage("ENUM_SHOW_SCORE_UI")
    _curr_score = 0;
end

function GetScore()
    return LuaHelper.GetCurScore(),LuaHelper.GetBstScore();
end

function SetBestScore(num)
    LuaHelper.SetBestScore(num);
end

function ResetGame()
    LuaHelper.ResetGame();
    SendGlobalMessage("ENUM_SHOW_SCORE_UI");
end


function StartAbility()
    log("Start Ability");
    LuaHelper.StartAbility();
end

function CloseAbility()
    log("Close Ability");
    LuaHelper.CloseAbility();
end

function GetNetStatus()
    return LuaHelper.getNetStatus();
end

function SetNetStatus(status)
    LuaHelper.setNetStatus(status);
end

function SetUserName(name)
    LuaHelper.setUserName(name);
end

function GetUserName()
    return LuaHelper.getUserName();
end

