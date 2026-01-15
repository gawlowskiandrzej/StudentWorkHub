DROP TRIGGER IF EXISTS trg_offers_cleanup_expired ON public.offers;

CREATE TRIGGER trg_offers_cleanup_expired
    AFTER INSERT OR UPDATE ON public.offers
        FOR EACH STATEMENT
            EXECUTE FUNCTION public.offers_cleanup_expired();