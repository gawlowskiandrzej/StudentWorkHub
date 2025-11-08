CREATE OR REPLACE FUNCTION public.get_dictionaries_text()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
SELECT jsonb_build_object(
    'skill.experienceLevel',
        COALESCE((SELECT jsonb_agg(level ORDER BY id) FROM public.experience_levels), '[]'::jsonb),
    'languages.language',
        COALESCE((SELECT jsonb_agg(language ORDER BY id) FROM public.languages), '[]'::jsonb),
    'languages.level',
        COALESCE((SELECT jsonb_agg(level ORDER BY id) FROM public.language_levels), '[]'::jsonb),
    'salary.currency',
        COALESCE((SELECT jsonb_agg(currency ORDER BY id) FROM public.currencies), '[]'::jsonb),
    'salary.period',
        COALESCE((SELECT jsonb_agg(period ORDER BY id) FROM public.salary_periods), '[]'::jsonb),
    'employment.types',
        COALESCE((SELECT jsonb_agg(type ORDER BY id) FROM public.employment_types), '[]'::jsonb),
    'employment.schedules',
        COALESCE((SELECT jsonb_agg(schedule ORDER BY id) FROM public.employment_schedules), '[]'::jsonb)
);
$$;