local _message_handler = {

    -- common message
    ["ENUM_SHOW_START_UI"] = { file = "ui/ui_start", func = "ShowStartUI(...)"},
    ["ENUM_SHOW_SCORE_UI"] = { file = "ui/ui_score", func = "ShowScoreUI(...)"},
    ["ENUM_SHOW_OVER_UI"] = { file = "ui/ui_over", func = "ShowOverUI(...)" },
    ["ENUM_DISABLE_SCORE_UI"] = { file = "ui/ui_score", func = "DisableScoreUI(...)" },
    ["ENUM_SHOW_LOGIN_UI"] = {file = "ui/ui_login", func = "ShowLoginUI(...)"}
}

local function ReceiveUIMessage(message,... )
    local handleEvents = _message_handler[message]
    if handleEvents then
        if handleEvents.file~=nil then
            require( handleEvents.file )
        end

        local func = handleEvents.func
        if type(func) == "string" then
            func = loadstring(func)
        end

        if func then
            func( luaType, ... )
        end
        func = nil
    end
end

function SendUIMessage( ... )
    ReceiveUIMessage(...)
end

function SendGlobalMessage( ... )
    ReceiveUIMessage(...)
    triggerGlobalEvent(...)
end