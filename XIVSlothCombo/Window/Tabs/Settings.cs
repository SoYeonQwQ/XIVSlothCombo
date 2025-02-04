﻿using ImGuiNET;
using System;
using System.Numerics;
using Dalamud.Logging;
using XIVSlothCombo.Services;

namespace XIVSlothCombo.Window.Tabs
{
    internal class Settings : ConfigWindow
    {
        internal static new void Draw()
        {
            ImGui.BeginChild("main", new Vector2(0, 0), true);
            ImGui.Text("This tab allows you to customise your options when enabling features.");

            #region SubCombos

            var hideChildren = Service.Configuration.HideChildren;

            if (ImGui.Checkbox("(隐藏子选项)Hide SubCombo Options", ref hideChildren))
            {
                Service.Configuration.HideChildren = hideChildren;
                Service.Configuration.Save();
            }

            if (ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.TextUnformatted("Hides the sub-options of disabled features.");
                ImGui.EndTooltip();
            }
            ImGui.NextColumn();

            #endregion

            #region Conflicting

            var hideConflicting = Service.Configuration.HideConflictedCombos;
            if (ImGui.Checkbox("(隐藏冲突的组合)Hide Conflicted Combos", ref hideConflicting))
            {
                Service.Configuration.HideConflictedCombos = hideConflicting;
                Service.Configuration.Save();
            }

            if (ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.TextUnformatted("Hides any combos that conflict with others you have selected.");
                ImGui.EndTooltip();
            }

            #endregion

            #region Combat Log

            var showCombatLog = Service.Configuration.EnabledOutputLog;

            if (ImGui.Checkbox("(输出到聊天框)Output Log to Chat", ref showCombatLog))
            {
                Service.Configuration.EnabledOutputLog = showCombatLog;
                Service.Configuration.Save();
            }

            if (ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.TextUnformatted("Every time you use an action, the plugin will print it to the chat.");
                ImGui.EndTooltip();
            }
            #endregion

            #region SpecialEvent

            var isSpecialEvent = DateTime.Now.Day == 1 && DateTime.Now.Month == 4;
            var slothIrl = isSpecialEvent && Service.Configuration.SpecialEvent;

            if (isSpecialEvent)

            {

                if (ImGui.Checkbox("Sloth Mode!?", ref slothIrl))
                {
                    Service.Configuration.SpecialEvent = slothIrl;
                    Service.Configuration.Save();
                }
            }

            else
            {
                Service.Configuration.SpecialEvent = false;
                Service.Configuration.Save();
            }

            float offset = (float)Service.Configuration.MeleeOffset;
            ImGui.PushItemWidth(75);

            var inputChangedeth = false;
            inputChangedeth |= ImGui.InputFloat("(近战偏移)Melee Distance Offset", ref offset);

            if (inputChangedeth)
            {
                Service.Configuration.MeleeOffset = (double)offset;
                Service.Configuration.Save();
            }

            if (ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.TextUnformatted("Offset of melee check distance for features that use it. For those who don't want to immediately use their ranged attack if the boss walks slightly out of range.");
                ImGui.EndTooltip();
            }

            #endregion

            #region Message of the Day

            var motd = Service.Configuration.HideMessageOfTheDay;

            if (ImGui.Checkbox("(隐藏登录时候的提示)Hide Message of the Day", ref motd))
            {
                Service.Configuration.HideMessageOfTheDay = motd;
                Service.Configuration.Save();
            }

            if (ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.TextUnformatted("Disables the Message of the Day message in your chat when you login.");
                ImGui.EndTooltip();
            }
            ImGui.NextColumn();

            #endregion


            #region 语言

            var language = Service.Configuration.Language;
            
            {
                // ImGui.SetNextItemWidth(50);
                if (ImGui.BeginCombo($"Language", language, ImGuiComboFlags.NoArrowButton))
                {

                    if (ImGui.Selectable("zh-CN", language == "zh-CN"))
                    {
                        Service.Configuration.Language =  "zh-CN";
                        // PluginLog.Information( "选择了中文");
                        Service.Configuration.Save();
                    }
                    
                    if (ImGui.Selectable("en", language == "en"))
                    {
                        Service.Configuration.Language =  "en";
                        // PluginLog.Information( "选择了en");
                        Service.Configuration.Save();
                    }
                    

                    ImGui.EndCombo();
                }
            }

            #endregion

            ImGui.EndChild();
        }
    }
}
