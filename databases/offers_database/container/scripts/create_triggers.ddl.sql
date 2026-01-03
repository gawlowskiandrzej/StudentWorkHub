DROP TRIGGER IF EXISTS trg_offers_cleanup_expired ON public.offers;

CREATE TRIGGER trg_offers_cleanup_expired_upd
    AFTER UPDATE OF expires ON public.offers
        FOR EACH STATEMENT
            EXECUTE FUNCTION public.offers_cleanup_expired();