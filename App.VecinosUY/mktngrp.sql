select
scope_seq_id,
course_external_name,
Course_description,
unit,
unit_module_id,
lesson,
lesson_module_id,
adaptative_module,
adaptative_module_id,
Unit_External_Name,
lesson_external_name,
am_external_name,
concepttypename,
Activity_Assessment_sq,
concept_id,
required,
concept_label,
CONcept_Title,
CONcept_Delivery,
Planned_Time,
points_possible,
listagg(MO_ID,';') within group (order by MO_ID) mo_id,
listagg(master_objective_text,';') within group (order by MO_ID)  as "master_objective_text",
listagg(big_idea_id,';') within group (order by big_idea_id)  as "big_idea_id",
listagg(big_idea_text,';') within group (order by big_idea_id)  as "big_idea_text",
listagg(keyword_id,';') within group (order by keyword_id)  as "keywords_id",
listagg(keyword_text,';') within group (order by keyword_text) as "keywords_text",
listagg(material_id,';') within group (order by material_id)  as "materials_id",
listagg(material_text,';') within group (order by material_id)  as "materials_text",
listagg(digital_material_id,';') within group (order by digital_material_id)  as "digital_material_id",
listagg(digital_material_text,';') within group (order by digital_material_id)  as "digital_material_text"
from
(
select
scope_seq_id,
Course_External_Name,
Course_description,
unit,
unit_module_id,
lesson,
lesson_module_id,
adaptative_module,
adaptative_module_id,
Unit_External_Name,
lesson_external_name,
am_external_name,
concepttypename,
name_kymt,
Activity_Assessment_sq,
concept_id,
required,
concept_label,
CONcept_Title,
CONcept_Delivery,
Planned_Time,
points_possible,
mo_id,
case when (sum(length(master_objective_text) + 1) over (partition by scope_seq_id,Course_External_Name,Course_description,Unit,Lesson,adaptative_module,Unit_External_Name,lesson_external_name,concepttypename,concept_id,required,concept_label,CONcept_Title,CONcept_Delivery,Planned_Time,points_possible) < 4000) then master_objective_text else (substr(master_objective_text,1,trunc(4000/(count(*) over (partition by scope_seq_id,Course_External_Name,Course_description,Unit,Lesson,adaptative_module,Unit_External_Name,lesson_external_name,concepttypename,concept_id,required,concept_label,CONcept_Title,CONcept_Delivery,Planned_Time,points_possible)))-4) || (case when length(master_objective_text) > 0 then '...' end) ) end as master_objective_text,
big_idea_id,
case when (sum(length(big_idea_text) + 1) over (partition by scope_seq_id,course_external_name,course_description,unit,lesson,adaptative_module,unit_external_name,lesson_external_name,concepttypename,concept_id,required,concept_label,concept_title,concept_delivery,planned_time,points_possible) < 4000) then big_idea_text else (substr(big_idea_text,1,trunc(4000/(count(*) over (partition by scope_seq_id,course_external_name,course_description,unit,lesson,adaptative_module,unit_external_name,lesson_external_name,concepttypename,concept_id,required,concept_label,concept_title,concept_delivery,planned_time,points_possible)))-4) || (case when length(big_idea_text) > 0 then '...' end) ) end as big_idea_text,
keyword_id,
case when (sum(length(keyword_text) + 1) over (partition by scope_seq_id,course_external_name,course_description,unit,lesson,adaptative_module,unit_external_name,lesson_external_name,concepttypename,concept_id,required,concept_label,concept_title,concept_delivery,planned_time,points_possible) < 4000) then keyword_text else (substr(keyword_text,1,trunc(4000/(count(*) over (partition by scope_seq_id,course_external_name,course_description,unit,lesson,adaptative_module,unit_external_name,lesson_external_name,concepttypename,concept_id,required,concept_label,concept_title,concept_delivery,planned_time,points_possible)))-4) || (case when length(keyword_text) > 0 then '...' end) ) end as keyword_text,
material_id,
case when (sum(length(material_text) + 1) over (partition by scope_seq_id,course_external_name,course_description,unit,lesson,adaptative_module,unit_external_name,lesson_external_name,concepttypename,concept_id,required,concept_label,concept_title,concept_delivery,planned_time,points_possible) < 4000) then material_text else (substr(material_text,1,trunc(4000/(count(*) over (partition by scope_seq_id,course_external_name,course_description,unit,lesson,adaptative_module,unit_external_name,lesson_external_name,concepttypename,concept_id,required,concept_label,concept_title,concept_delivery,planned_time,points_possible)))-4) || (case when length(material_text) > 0 then '...' end) ) end as material_text,
digital_material_id,
case when (sum(length(digital_material_text) + 1) over (partition by scope_seq_id,course_external_name,course_description,unit,lesson,adaptative_module,unit_external_name,lesson_external_name,concepttypename,concept_id,required,concept_label,concept_title,concept_delivery,planned_time,points_possible) < 4000) then digital_material_text else (substr(digital_material_text,1,trunc(4000/(count(*) over (partition by scope_seq_id,course_external_name,course_description,unit,lesson,adaptative_module,unit_external_name,lesson_external_name,concepttypename,concept_id,required,concept_label,concept_title,concept_delivery,planned_time,points_possible)))-4) || (case when length(digital_material_text) > 0 then '...' end) ) end as digital_material_text
from
(
SELECT 
      ss.scope_seq_id,
      course.display_name AS Course_External_Name,
      REGEXP_REPLACE(course.description,'<[^<]*>', '') AS Course_description,
      t.subtype_id as subtypeid,
    case t.subtype_id 
    when 1 then t3.seq_num 
    when 5 then t2.seq_num 
    else null
    end as unit,
    
    case t.subtype_id
    when 1 then t3.module_id
    when 5 then t2.module_id
      else null
    end as unit_module_id,
   
    case t.subtype_id
    when 1 then t2.seq_num
    when 5 then t.seq_num - 1 
    else null
    end as lesson,
    
    case t.subtype_id
      when 1 then t2.module_id
    when 5 then t.module_id
    else null 
   end as lesson_module_id,
   
   case t.subtype_id
    when 1 then t.seq_num
    when 5 then null
        else null
   end as adaptative_module,
   
   case t.subtype_id
    when 1 then t.module_id
    when 5 then null
        else null 
   end as adaptative_module_id,
   
      unit.display_name AS Unit_External_Name,
      lesson.display_name as lesson_external_name,
      am.display_name as am_external_name,
      mod_mos.concepttypename,
      mod_mos.name_kymt,
      mod_mos.seq_num as Activity_Assessment_sq,
      mod_mos.concept_id as concept_id,
      mod_mos.required as required,
      mod_mos.label_name as concept_label,
      mod_mos.cONcept_title AS CONcept_Title,
      mod_mos.delivery AS CONcept_Delivery,
      mod_mos.planned_time AS Planned_Time,
      mod_mos.points_possible as points_possible,
      case when (mod_mos.mo_text is not null) then mod_mos.mo_id end as mo_id,
      regexp_replace(mod_mos.mo_text,'<[^<]*>', '') as master_objective_text,
      mod_mos.idea_id as big_idea_id,
      regexp_replace(mod_mos.idea_text,'<[^<]*>', '') as big_idea_text,
      case when (mod_mos.name_kymt = 'Keyword') then mod_mos.mo_id end as keyword_id,
     case when ( mod_mos.name_kymt = 'Keyword' and (
                (
                (dbms_lob.getlength(regexp_replace(xkwlang.text,'<[^<]*>', '')))+dbms_lob.getlength(regexp_replace(xdeflang.text,'<[^<]*>', '')) is null)
                or
                (
                (dbms_lob.getlength(regexp_replace(xkwlang.text,'<[^<]*>', ''))+dbms_lob.getlength(regexp_replace(xdeflang.text,'<[^<]*>', ''))) < 3999 )
                )) then
          concat
            (
              dbms_lob.substr(regexp_replace(xkwlang.text,'<[^<]*>', ''), dbms_lob.getlength(regexp_replace(xkwlang.text,'<[^<]*>', '')), 1)
              ||
              ':' ,
              dbms_lob.substr(regexp_replace(xdeflang.text,'<[^<]*>', ''), dbms_lob.getlength(regexp_replace(xdeflang.text,'<[^<]*>', '')), 1)
            )
          else
               null
          end as keyword_text,
      case when (mod_mos.name_kymt = 'Material') then mod_mos.mo_id end as material_id,
      case when (mod_mos.name_kymt = 'Material') then dbms_lob.substr(regexp_replace(xmatlang.title,'<[^<]*>', ''), dbms_lob.getlength(regexp_replace(xmatlang.title,'<[^<]*>', '')), 1)  end as material_text,
      case when (mod_mos.name_kymt = 'Digital Material') then dm.media_asset_id end as digital_material_id,
      case when (mod_mos.name_kymt = 'Digital Material') then dbms_lob.substr(regexp_replace(cfgmat.display_name,'<[^<]*>', ''), dbms_lob.getlength(regexp_replace(cfgmat.display_name,'<[^<]*>', '')), 1) end as digital_material_text
  FROM  calms.scopesequence ss
  INNER JOIN calms.sstreeview t       ON ss.module_id = t.root_module_id  and ss.scope_seq_id  = 2907
  INNER JOIN calms.sstreeview t2      ON t.parent_module_id = t2.module_id AND t.root_module_id = t2.root_module_id 
  left outer JOIN calms.sstreeview t3      ON t2.parent_module_id = t3.module_id AND t2.root_module_id = t3.root_module_id and t3.module_id = t2.parent_module_id
  INNER JOIN calms.xmodulelang course ON ss.module_id = course.module_id
  inner join calms.xmodulelang unit   on (t.parent_module_id = unit.module_id and t.subtype_id = 5) Or (t3.module_id = unit.module_id and t.subtype_id = 1)
  inner join calms.module m           on t.module_id = m.module_id
  inner join calms.xmodulelang lesson on (m.module_id = lesson.module_id and t.subtype_id = 5) or (t2.module_id = lesson.module_id and t.subtype_id = 1)
  left outer join calms.xmodulelang am on am.module_id = t.module_id and t.subtype_id = 1
  left outer join
  (
  SELECT
          mact.module_id as module_id,
          ct.name as name_kymt,
          'Activity' AS conceptTypeName,
          mact.activity_id AS concept_id,
          sn.seq_num AS seq_num,
          CASE WHEN (sn.is_optiONal = 0) THEN 'Required' ELSE 'Optional' END AS required,
          alt.name as label_name,
          REGEXP_REPLACE(xal.title,'<[^<]*>', '') AS cONcept_title,
          dt.name AS delivery,
          act.planned_time/60000 AS planned_time,
          actmo.resource_id as mo_id,
          dbms_lob.substr( REGEXP_REPLACE(xmo.text,'<[^<]*>', ''), 4000, 1 ) AS mo_text,
          xil.idea_id as idea_id,
          dbms_lob.substr(REGEXP_REPLACE(xil.text,'<[^<]*>', ''), 4000, 1) AS idea_text,
          NULL AS points_possible,
          ct.id,
          actmo.resource_cfg_id
     FROM calms.scopesequence  ss
    INNER JOIN calms.sstreeview t                ON ss.module_id = t.root_module_id and ss.scope_seq_id = 2907
    inner join calms.ca_mod_view  mact           on t.module_id = mact.module_id
     LEFT JOIN calms.Content_Resource_View actmo        ON (mact.activity_id = actmo.cONtent_id and actmo.content_type_id = 0 AND actmo.resource_cONtent_type_id in (8,14,16,20) )
    INNER JOIN calms.activity act                ON mact.activity_id = act.activity_id
    inner join calms.xactivitylang xal           on mact.activity_id = xal.activity_id
    INNER JOIN calms.activitylabeltype alt       ON act.activity_label_id = alt.id
    INNER JOIN calms.deliverytype dt             ON act.delivery_type_id = dt.id
    inner join calms.xsequencenodeactivity xsna  on mact.activity_id = xsna.activity_id
    inner join calms.sequencenode sn             on xsna.sequence_node_id = sn.sequence_node_id and sn.parent_module_id = mact.module_id
     left join calms.xmasterobjectivelang xmo    on (actmo.resource_id = xmo.master_objective_id and actmo.resource_content_type_id = 8)
     left join calms.idea_mo_view imv            on (actmo.resource_id = imv.master_objective_id)
     left join calms.xidealang xil               on imv.idea_id = xil.idea_id
     left join calms.concepttype ct              on ct.id = actmo.resource_content_type_id
    where t.subtype_id in (1,5)
UNION --activity wrapper
  SELECT distinct
          t.parent_module_id as module_id,
          ct.name as name_kymt,
          'Adaptative Wrapper' AS conceptTypeName,
          xaw.concept_id AS concept_id,
          t.seq_num AS seq_num,
          CASE WHEN (t.is_optiONal = 0) THEN 'Required' ELSE 'Optional' END AS required,
          alt.name as label_name,
          REGEXP_REPLACE(xal.title,'<[^<]*>', '') AS cONcept_title,
          dt.name AS delivery,
          act.planned_time/60000 AS planned_time,
          actmo.resource_id as mo_id,
          dbms_lob.substr( REGEXP_REPLACE(xmo.text,'<[^<]*>', ''), 4000, 1 ) AS mo_text,
          xil.idea_id as idea_id,
          dbms_lob.substr(REGEXP_REPLACE(xil.text,'<[^<]*>', ''), 4000, 1) AS idea_text,
          NULL AS points_possible,
          ct.id,
          actmo.resource_cfg_id 
     FROM calms.scopesequence  ss
    INNER JOIN calms.sstreeview t                ON ss.module_id = t.root_module_id and ss.scope_seq_id = 2907 
    inner join calms.xseqnodeadaptivewrapper xsaw  on xsaw.sequence_node_id = t.sequence_node_id
    inner join calms.xadapwraplearnertypeconcept xaw on xaw.adaptive_wrapper_id = xsaw.adaptive_wrapper_id
     LEFT JOIN calms.Content_Resource_View actmo        ON (xaw.concept_id = actmo.cONtent_id and actmo.content_type_id = 0 AND actmo.resource_cONtent_type_id in (8,14,16,20) )
    INNER JOIN calms.activity act                ON xaw.concept_id = act.activity_id
    inner join calms.xactivitylang xal           on xaw.concept_id = xal.activity_id
    INNER JOIN calms.activitylabeltype alt       ON act.activity_label_id = alt.id
    INNER JOIN calms.deliverytype dt             ON act.delivery_type_id = dt.id
    inner join calms.xsequencenodeactivity xsna  on xaw.concept_id = xsna.activity_id
    inner join calms.sequencenode sn             on xsna.sequence_node_id = sn.sequence_node_id --and sn.parent_module_id = t.parent_module_id
     left join calms.xmasterobjectivelang xmo    on (actmo.resource_id = xmo.master_objective_id and actmo.resource_content_type_id = 8)
     left join calms.idea_mo_view imv            on (actmo.resource_id = imv.master_objective_id)
     left join calms.xidealang xil               on imv.idea_id = xil.idea_id
     left join calms.concepttype ct              on ct.id = actmo.resource_content_type_id
  UNION
    select mtest.module_id as module_id,
            ct.name as name_kymt,
           'Asseessment' AS conceptTypeName,
           mtest.assessment_id AS concept_id,
           sn.seq_num AS seq_num,
           CASE WHEN (sn.is_optiONal = 0) THEN 'Required' ELSE 'Optional' END AS required,
           alt.name as label_name,
           REGEXP_REPLACE(ASmt.title,'<[^<]*>', '') AS cONcept_title,
           adf.name AS delivery,
           ASmt.ASsessment_time/60000 AS planned_time,
           testmo.resource_id as mo_id,
           dbms_lob.substr(REGEXP_REPLACE( xmo.text,'<[^<]*>', ''), 4000, 1 ) AS mo_text,
           xil.idea_id as idea_id,
           dbms_lob.substr(REGEXP_REPLACE(xil.text,'<[^<]*>', ''), 4000, 1) AS idea_text,
           ap.points AS points_possible,
           ct.id,
           testmo.resource_cfg_id
      from calms.scopesequence  ss
     inner join  calms.sstreeview t                     on ss.module_id = t.root_module_id and ss.scope_seq_id  = 2907
     inner join  calms.aa_mod_view    mtest             on t.module_id = mtest.module_id
      LEFT JOIN   calms.CONtent_With_Quest_Resrc_View testmo            ON (mtest.assessment_id = testmo.cONtent_id AND testmo.content_type_id = 1 AND testmo.resource_cONtent_type_id in (8,14,16,20))
     INNER JOIN  calms.ASsessment ASmt                  ON mtest.assessment_id = ASmt.assessment_id
     inner join  calms.assessmentlabeltype alt          on asmt.label_type_id = alt.id
     inner join  calms.xassessmentdeliveryformats xadf  on mtest.assessment_id = xadf.assessment_id
     inner join  calms.assessmentdeliveryformattype adf on xadf.id = adf.id
     left outer JOIN (
        select aqv.assessment_id, sum(ai.points) as points
          from calms.assessment_questions_view aqv
         INNER JOIN calms.ASsessmentitem ai ON aqv.ASsessment_item_id = ai.ASsessment_item_id
         GROUP BY aqv.assessment_id) ap ON mtest.assessment_id = ap.assessment_id
     inner join calms.xsequencenodeassessment xsna  on asmt.assessment_id = xsna.assessment_id
     inner join calms.sequencenode sn               on xsna.sequence_node_id = sn.sequence_node_id and sn.parent_module_id = mtest.module_id
      left join calms.xmasterobjectivelang xmo      on (testmo.resource_id = xmo.master_objective_id and testmo.resource_content_type_id = 8)
      LEFT JOIN calms.idea_mo_view imv              ON (testmo.resource_id = imv.mASter_objective_id)
      left join calms.xidealang xil                 on imv.idea_id = xil.idea_id
      LEFT JOIN calms.cONceptType ct                ON ct.id = testmo.resource_cONtent_type_id
     where t.subtype_id in(1,5)) mod_mos   on (m.module_id = mod_mos.module_id and t.subtype_id = 5) OR (t.module_id = mod_mos.module_id and t.subtype_id = 1)
  left outer join calms.keyworddefinition def on def.definition_id = mod_mos.mo_id and mod_mos.id = 14
  left outer join calms.xkeywordlang xkwlang on (xkwlang.keyword_id  = def.keyword_id and xkwlang.language_id = 1)
  left outer join calms.xkeyworddefinitionlang xdeflang on (xdeflang.definition_id = def.definition_id and xdeflang.language_id   = 1 )
  left outer join calms.material mat on mat.material_id= mod_mos.mo_id and mod_mos.id = 16
  left outer join calms.xmateriallang xmatlang on xmatlang.material_id = mat.material_id and xmatlang.language_id = 1
  left outer join calms.configuredmaterial cfgmat on cfgmat.configured_material_id = mod_mos.resource_cfg_id and mod_mos.id = 20
  left outer join calms.digitalmaterial dm on mod_mos.mo_id = dm.digital_material_id and mod_mos.id = 20
) 
union
select
    ss.scope_seq_id as scope_seq_id,
    course.display_name AS Course_External_Name,
    regexp_replace(course.description,'<[^<]*>', '') as course_description,
    t.seq_num as unit,
    t.module_id as unit_module_id,
    0 as lesson,
    0 as lesson_module_id,
    0 as adaptative_module,
    0 as adaptative_module_id,
    unit.display_name as unit_external_name,
    null as lesson_external_name,
    null as am_external_name,
    'Unit Opener' as concepttypename,
    null as name_kymt,
    1 as Activity_Assessment_sq,
    mact.activity_id as concept_id,
    'Required' as required,
    alt.name as concept_label,
    regexp_replace(xal.title,'<[^<]*>', '') as concept_title,
    null as concept_delivery,
    null as Planned_Time,
    null as points_possible,
    null as mo_id,
    null as master_objective_text,
    null as big_idea_id,
    null as big_idea_text,
    null as keyword_id,
    null as keyword_text,
    null as material_id,
    null as material_text,
    null as digital_material_id,
    null as digital_material_text
    from calms.scopesequence  ss
    inner join calms.sstreeview t                on ss.module_id = t.parent_module_id and t.concept_type_id = 5 and t.subtype_id = 4  and ss.scope_seq_id  = 2907
    inner join calms.ca_mod_view  mact           on t.module_id = mact.module_id
    INNER JOIN calms.activity act                ON mact.activity_id = act.activity_id
    inner join calms.xactivitylang xal           on mact.activity_id = xal.activity_id
    inner join calms.activitylabeltype alt       on act.activity_label_id = alt.id
    inner join calms.xmodulelang course on ss.module_id = course.module_id
    inner join calms.xmodulelang unit   on t.module_id = unit.module_id
    and t.subtype_id = 4
      )
group by
scope_seq_id,
course_external_name,
Course_description,
unit,
unit_module_id,
lesson,
lesson_module_id,
adaptative_module,
adaptative_module_id,
Unit_External_Name,
lesson_external_name,
am_external_name,
concepttypename,
Activity_Assessment_sq,
concept_id,
required,
concept_label,
CONcept_Title,
CONcept_Delivery,
planned_time,
points_possible
order by
scope_seq_id,
course_external_name,
Course_description,
Unit,
unit_module_id,
lesson,
lesson_module_id,
adaptative_module,
adaptative_module_id,
Unit_External_Name,
lesson_external_name,
am_external_name,
Activity_Assessment_sq,
concepttypename,
concept_id,
required,
concept_label,
CONcept_Title,
CONcept_Delivery,
planned_time,
points_possible
;