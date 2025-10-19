create sequence chat_report_format_google_sheets_id_seq
    as integer;

create sequence chat_report_template_id_seq;

create table chat_report_format
(
    id          serial
        constraint chat_report_format_pk
            primary key,
    code        varchar(20) not null
        constraint chat_report_format_unique_key
            unique,
    name        varchar(50) not null,
    description varchar(300)
) using ???;

create table chat_report_template
(
    id          integer default nextval('client_template.chat_report_template_id_seq'::regclass) not null
        constraint chat_report_template_pk
            primary key,
    name        varchar(50)                                                                      not null,
    description varchar(300),
    create_date timestamp                                                                        not null,
    format_id   integer                                                                          not null
        constraint chat_report_template_format_fk
            references chat_report_format
) using ???;

create table chat_report_template_column_type
(
    name        varchar(50) not null,
    description varchar(300),
    id          serial
        constraint chat_report_template_column_type_pk
            primary key
) using ???;

create table chat_report_template_column
(
    id          serial
        constraint chat_report_template_column_pk
            primary key,
    template_id integer     not null
        constraint chat_report_template_column_template_fk
            references chat_report_template,
    type_id     smallint    not null
        constraint chat_report_template_column_type_fk
            references chat_report_template_column_type,
    name        varchar(50) not null,
    description varchar(300),
    create_date timestamp   not null
) using ???;

create table google_credential
(
    id                serial
        constraint google_credential_pk
            primary key,
    service_account   text        not null,
    private_key       bytea       not null,
    name              varchar(50) not null,
    create_date       timestamp   not null,
    n8n_credential_id varchar(500)
        constraint google_credential_unique_key
            unique
) using ???;

create table chat_report_instance_google_sheets
(
    id            integer default nextval('client_template.chat_report_format_google_sheets_id_seq'::regclass) not null
        constraint chat_report_instance_google_sheets_pk
            primary key,
    credential_id integer                                                                                      not null
        constraint chat_report_format_google_sheets_credential_fk
            references google_credential,
    sheet_name    varchar(50)                                                                                  not null,
    url           text                                                                                         not null
) using ???;

alter sequence chat_report_format_google_sheets_id_seq owned by chat_report_instance_google_sheets.id;

create table chat_report_instance
(
    id               serial
        constraint chat_report_instance_pk
            primary key,
    name             varchar(50) not null,
    description      varchar(300),
    template_id      integer     not null
        constraint chat_report_instance_template_fk
            references chat_report_template,
    google_sheets_id integer
        constraint chat_report_instance_google_sheets_fk
            references chat_report_instance_google_sheets
) using ???;

create table human_agent
(
    id          serial
        constraint human_agent_pk
            primary key,
    first_name  varchar(50)  not null,
    last_name   varchar(50)  not null,
    email       varchar(100) not null,
    phone       varchar(50)  not null
        constraint human_agent_unique_key
            unique,
    create_date timestamp    not null
) using ???;

create table instructions
(
    id          serial
        constraint instructions_pk
            primary key,
    name        varchar(50) not null,
    create_date timestamp   not null,
    description varchar(300)
) using ???;

create table instructions_section
(
    id          serial
        constraint instructions_section_pk
            primary key,
    iid         integer     not null
        constraint instructions_section_instruction_fk
            references instructions,
    name        varchar(50) not null,
    description varchar(300),
    "order"     integer     not null,
    priority    smallint    not null,
    create_date timestamp   not null,
    text        text        not null
) using ???;

create table openai_credential
(
    id                serial
        constraint openai_credential_pk
            primary key,
    token             bytea       not null
        constraint openai_credential_unique_key
            unique,
    name              varchar(50) not null
        constraint openai_credential_unique_key2
            unique,
    create_date       timestamp   not null,
    model_id          varchar(20) not null,
    n8n_credential_id varchar(500)
) using ???;

create table rule
(
    text        text        not null,
    description varchar(300),
    create_date timestamp   not null,
    name        varchar(50) not null,
    id          serial
        constraint rule_pk
            primary key
) using ???;

create table instructions_rule
(
    id          serial
        constraint instructions_rule_pk
            primary key,
    iid         integer   not null
        constraint instructions_rule_instruction_fk
            references instructions,
    rid         integer   not null
        constraint instructions_rule_rule_fk
            references rule,
    priority    smallint  not null,
    create_date timestamp not null,
    constraint instructions_rule_unique_key
        unique (rid, iid)
) using ???;

create table rule_set
(
    name        varchar(50) not null,
    description varchar(300),
    create_date timestamp   not null,
    id          serial
        constraint rule_set_pk
            primary key
) using ???;

create table rule_set_rules
(
    id   serial
        constraint rule_set_rules_pk
            primary key,
    rid  integer not null
        constraint rule_set_rules_rule_fk
            references rule,
    rsid integer not null
        constraint rule_set_rules_rule_set_fk
            references rule_set,
    constraint rule_set_rules_unique_key
        unique (rid, rsid)
) using ???;

create table "user"
(
    id          serial
        constraint user_pk
            primary key,
    first_name  varchar(50)  not null,
    last_name   varchar(50)  not null,
    email       varchar(100) not null,
    phone       varchar(50)  not null
        constraint user_unique_key
            unique,
    create_date timestamp    not null
) using ???;

create table twilio_credential
(
    id                serial
        constraint twilio_credential_pk
            primary key,
    token             bytea        not null
        constraint twilio_credential_unique_key
            unique,
    name              varchar(50)  not null
        constraint twilio_credential_unique_key2
            unique,
    create_date       timestamp    not null,
    n8n_credential_id varchar(500)
        constraint twilio_credential_unique_key3
            unique,
    sid               varchar(500) not null
) using ???;

create table workflow
(
    id          serial
        constraint workflow_pk
            primary key,
    code        varchar(20) not null
        constraint workflow_unique_key
            unique,
    name        varchar(50) not null,
    description varchar(300),
    url         text,
    create_date timestamp   not null,
    json        json
) using ???;

create table ai_agent
(
    id              serial
        constraint ai_agent_pk
            primary key,
    code            varchar(20) not null
        constraint ai_agent_unique_key
            unique,
    wid             integer     not null
        constraint ai_agent_workspace__fk
            references workflow,
    name            varchar(50) not null,
    description     varchar(300),
    create_date     timestamp   not null,
    instructions_id integer     not null
        constraint ai_agent_instructions_id_fk
            references instructions
) using ???;

create table session
(
    id             serial
        constraint session_pk
            primary key,
    uid            integer   not null
        constraint session_user__fk
            references "user",
    start_date     timestamp not null,
    end_date       timestamp not null,
    ai_agent_id    integer
        constraint session_ai_agent_fk
            references ai_agent,
    human_agent_id integer
        constraint session_human_agent_fk
            references human_agent
) using ???;

create table chat
(
    id             bigserial
        constraint chat_pk
            primary key,
    uid            integer   not null
        constraint chat_user_fk
            references "user",
    ai_agent_id    integer
        constraint chat_ai_agent_fk
            references ai_agent,
    human_agent_id integer
        constraint chat_human_agent_fk
            references human_agent,
    start_date     timestamp not null,
    end_date       timestamp not null,
    sid            integer
        constraint chat_session_fk
            references session
) using ???;

create table chat_message
(
    id       bigserial
        constraint chat_message_pk
            primary key,
    cid      bigint    not null
        constraint chat_message_chat_fk
            references chat,
    date     timestamp not null,
    is_agent boolean   not null,
    text     text      not null
) using ???;

create table chat_report
(
    id          serial
        constraint chat_report_pk
            primary key,
    instance_id integer   not null
        constraint chat_report_instance_fk
            references chat_report_instance,
    cid         bigint    not null
        constraint chat_report_chat_fk
            references chat,
    data        bytea,
    create_date timestamp not null,
    name        varchar(50)
) using ???;

create table chat_report_column_value
(
    column_id integer not null
        constraint chat_report_column_value_column_fk
            references chat_report_template_column,
    value     bytea   not null,
    report_id integer not null
        constraint chat_report_column_value_report_fk
            references chat_report,
    id        serial
        constraint chat_report_column_value_pk
            primary key,
    constraint chat_report_column_value_unique_key
        unique (report_id, column_id)
) using ???;

