using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eblue.Utils
{
    public static class ConstantsTools
    {
        public const string UserGenericPictureData = "data:application/octet-stream;base64,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";
        public const string UserGenericSignData = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHgAAAAoCAYAAAA16j4lAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsEAAA7BAbiRa+0AAAGuSURBVHhe7Za7koQgEEX3z/04c1Jic1NjFqVbHsJYu+7W9Ny6p6oDQZHq42Xma5qmwMKtQzDBhILBoWBwKBgcCgaHgsGhYHAoGBwKBoeCwaFgcCgYHAoGh4LBoWBwKBgcU4JXP4d5juVXGTHC6tO+Zh+M7ewWO4K3JbjYRLdsMmAM6/sbYEewpOT9DdzC4jppFcHmTpcbKPgCBf8PIjj3bw3+EL5Kw1PV/U336Nw8u1B+H+dv+l7VgypRS59rx2O5JY7uUxT8iCSjFJTlpZ7qtSarvVY5skZzImyLbyTmhKZ3t+s0CdZxFf4hmBB8NPjSOBFYjFcfQedI3xaXx2T+sm4vidXpMRKcSO/oz1nETIJT4zoJHgiuZCqN9HS/lkhR8Z26E1wn/TMwI1gbn4P1+wSX4dzpJbv6MCpGgmU/7eLGMSc4N/5G8GW+FrNLPdeq1pbnXiSxfo/AP1kP+bHgiDb9rFKaCpeqxKjksopny3X1/RT8EGng+Oh8M9b3N8CO4EhKqMGUyOny6li3iinB5O+hYHAoGBwKBoeCwaFgcCgYHAoGh4LBOQWzUGsK3zHGaQIv6rKZAAAAAElFTkSuQmCC";
        public const string ContentPlaceHolderPageBodyID = "ContentPlaceHolder1";
        public const string CssClassFormControl = "form-control";
        public const string CssClassGridview = "gridview table table-bordered table-striped";
        //⓪①②③④⑤⑥⑦⑧⑨⓿❶❷❸❹❺❻❼❽❾
        public static readonly char[] WhiteNumbers = "⓪①②③④⑤⑥⑦⑧⑨⑩⑪⑫⑬⑭⑮⑯⑰⑱⑲⑳".ToCharArray();
        public static readonly char[] BlackNumbers = "⓿❶❷❸❹❺❻❼❽❾❿⓫⓬⓭⓮⓯⓰⓱⓲⓳⓴".ToCharArray();
        public const string tagier_project_section_a0 = "project.section.a0"; //[]                  --status
        public const string tagier_project_section_a1 = "project.section.a1"; 
        public const string tagier_project_section_a2 = "project.section.a2";
        public const string tagier_project_section_a3 = "project.section.a3";
        public const string tagier_project_section_a4 = "project.section.a4";
        public const string tagier_project_section_a5 = "project.section.a5";
        public const string tagier_project_section_a6 = "project.section.a6";
        public const string tagier_project_section_a7 = "project.section.a7";
        public const string tagier_project_section_a8 = "project.section.a8";//[]                   --budges
        public const string tagier_project_section_a9 = "project.section.a9";

        //⑩⑪⑫⑬⑭⑮⑯⑰⑱⑲⑳❿⓫⓬⓭⓮⓯⓰⓱⓲⓳⓴
        //section to show project.status.-process.progress bits and role actions

        public const string tagier_project_section_b0 = "project.section.b0"; //scope project follows
        public const string tagier_project_section_b1 = "project.section.b1"; //comments            --signs
        public const string tagier_project_section_b2 = "project.section.b2"; //revisions           --notes
        public const string tagier_project_section_b3 = "project.section.b3"; //signs               --assents
        public const string tagier_project_section_b4 = "project.section.b4"; //aproval             --objetions
        public const string tagier_project_section_b5 = "project.section.b5"; //rejects
        public const string tagier_project_section_b6 = "project.section.b6"; //auths
        public const string tagier_project_section_b7 = "project.section.b7"; //ejecutions
        public const string tagier_project_section_b8 = "project.section.b8"; //enclose
        public const string tagier_project_section_b9 = "project.section.b9"; //locked

    }
}